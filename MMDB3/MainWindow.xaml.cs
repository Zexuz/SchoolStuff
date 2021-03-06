﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MMDB.MovieDatabase.Services;
using MMDB.MovieDatabase.ValueObjects;


namespace MMDB3 {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly FreeTextSearchService _searchService;

        public MainWindow() {
            InitializeComponent();
            _searchService = new FreeTextSearchService();
        }


        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var searchText = SearchTextBox.Text;
            IEnumerable<SearchResultItem> everybody;

            if (searchText.Trim().Length > 0)
                everybody = _searchService.Search(searchText, true);
            else
                everybody = Enumerable.Empty<SearchResultItem>();
            SearchItems.ItemsSource = everybody;
        }

    }

}