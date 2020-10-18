using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.Logging;
using Catel.MVVM;
using Catel.Services;
using DevExpress.Xpf.Grid;
using SmallPdfTest.Models;
using SmallPdfTest.Repositories;
using SmallPdfTest.Services;

namespace SmallPdfTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly ICommonRepository commonRepository;
        private readonly ICurrencyService currencyService;
        private readonly IIsolatedStorageService iSolatedStorageService;
        private readonly IMessageService messageService;

        public MainWindowViewModel(ICurrencyService currencyService, ICommonRepository commonRepository,
            IMessageService messageService, IIsolatedStorageService iSolatedStorageService)
        {
            this.currencyService = currencyService;
            this.commonRepository = commonRepository;
            this.messageService = messageService;
            this.iSolatedStorageService = iSolatedStorageService;
            ConvertCurrencyCommand = new TaskCommand<CellValueChangedEventArgs>(OnConvertCurrencyExecute);
            DeleteRowCommand = new TaskCommand<EditGridCellData>(OnDeleteRowCommand);
        }

        public IList<ComboItem> Currencies { get; set; }

        public RowItem SelectedItem { get; set; }
        public FastBindingList<RowItem> Items { get; set; }
        public TaskCommand<CellValueChangedEventArgs> ConvertCurrencyCommand { get; }

        public TaskCommand<EditGridCellData> DeleteRowCommand { get; }

        public override string Title => "Welcome to SmallPdfTest - Ponzano Paolo for SmallPDF";

        private async Task OnDeleteRowCommand(EditGridCellData o)
        {
            var res = await messageService.ShowAsync("Do you really want to delete the selected row?",
                "Confirm cancellation", MessageButton.YesNo);

            if (res == MessageResult.Yes)
            {
                if (!(o.Row is RowItem rowItem)) return;

                Items.Remove(rowItem);
            }
        }

        private async Task OnConvertCurrencyExecute(CellValueChangedEventArgs o)
        {
            try
            {
                if (!(o.Row is RowItem rowItem) || o.Cell.Property == "ConvertedValue")
                    return; //I don't want to threat it if it's just the converted value... this should not happen since the prop is ROnly

                //Only if all the required fields are filled I procede to call the webservice
                if (!string.IsNullOrEmpty(rowItem.From) && !string.IsNullOrEmpty(rowItem.To) && rowItem.Amount.HasValue)
                {
                    var res = await currencyService.GetConvertedValueAsync(rowItem.From, rowItem.To,
                        rowItem.Amount.Value);

                    if (res.HasValue)
                    {
                        rowItem.ConvertedValue = res.Value;

                        //This is to update the Converted Value cell before leaving the current value cekk
                        o.Source.Grid.SetCellValue(o.RowHandle, o.Source.Grid.Columns["ConvertedValue"], res.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            Currencies = commonRepository.GetCurrencies();

            try
            {
                var content = iSolatedStorageService.Load<FastBindingList<RowItem>>("CurrenciesData.dat");

                if (content != null)
                {
                    Items = content;
                    SelectedItem = Items.FirstOrDefault();

                    await RefreshLoadedCurrencyConversion();
                }
                else
                {
                    Items = new FastBindingList<RowItem>();
                }
            }
            catch
            {
                //This is wanted, since it can be that the file is not present and so I don't need to care about the exception
                Items = new FastBindingList<RowItem>();
            }
        }

        private async Task RefreshLoadedCurrencyConversion()
        {
            foreach (var rowItem in Items)
                if (rowItem.Amount.HasValue)
                {
                    var value = await currencyService.GetConvertedValueAsync(rowItem.From, rowItem.To,
                        rowItem.Amount.Value);
                    rowItem.ConvertedValue = value;
                }
        }

        protected override async Task CloseAsync()
        {
            iSolatedStorageService.Save("CurrenciesData.dat", Items);

            await base.CloseAsync();
        }
    }
}