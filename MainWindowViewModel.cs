using Module4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Module4
{
	public class MainWindowViewModel : BaseViewModel
	{
		MovieLogic movieLogic = new MovieLogic();

		private ObservableCollection<Movie> _MovieCollection;

		public ObservableCollection<Movie> MovieCollection
		{
			get { return _MovieCollection; }
			set 
			{ 
				_MovieCollection = value;
				OnPropertyRaised("MovieCollection");
			}
		}

		private Movie _SelectedMovie;

		public Movie SelectedMovie
		{
			get { return _SelectedMovie; }
			set 
			{ 
				_SelectedMovie = value;
				OnPropertyRaised("SelectedMovie");
			}
		}

        public ICommand AddCommand { get; set; }
		public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public MainWindowViewModel()
        {
			AddCommand = new RelayCommand(AddCommandHandler, CanAddCommand);
			UpdateCommand = new RelayCommand(UpdateCommandHandler, CanUpdateCommand);
			DeleteCommand = new RelayCommand(DeleteCommandHandler, CanDeleteCommand);
			movieLogic.AddMovie(new Movie(1, "The Shawshank Redemption", Genre.Drama, 1994, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", 142, 9.3, "Frank Darabont", new ObservableCollection<string> { "Tim Robbins", "Morgan Freeman", "Bob Gunton" }, true));
			movieLogic.AddMovie(new Movie(2, "The Godfather", Genre.Drama, 1972, "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", 175, 9.2, "Francis Ford Coppola", new ObservableCollection<string> { "Marlon Brando", "Al Pacino", "James Caan" }, true));
			movieLogic.AddMovie(new Movie(3, "The Dark Knight", Genre.Action, 2008, "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.", 152, 9.0, "Christopher Nolan", new ObservableCollection<string> { "Christian Bale", "Heath Ledger", "Aaron Eckhart" }, true));
			MovieCollection = movieLogic.moviesCollection;
		}

		private void DeleteCommandHandler(object obj)
		{
			if(SelectedMovie !=null)
				MovieCollection.Remove(SelectedMovie);
		}

		private bool CanDeleteCommand(object obj)
		{
			return true;
		}

		private void AddCommandHandler(object obj)
		{
			AddUpdateMovieDialog addUpdateMovieDialog = new AddUpdateMovieDialog(SelectedMovie);
			var dailog = addUpdateMovieDialog.ShowDialog();
			var vm = addUpdateMovieDialog.DataContext as AddUpdateMovieViewModel;
			MovieCollection.Add(vm.AddedMovie);
		}

		private bool CanAddCommand(object obj)
		{
			return true;
		}


		private void UpdateCommandHandler(object obj)
		{
			AddUpdateMovieDialog addUpdateMovieDialog = new AddUpdateMovieDialog(SelectedMovie);
			var dailog = addUpdateMovieDialog.ShowDialog();
			var vm = addUpdateMovieDialog.DataContext as AddUpdateMovieViewModel;
			MovieCollection.Remove(SelectedMovie);
			MovieCollection.Add(vm.AddedMovie);
		}

		private bool CanUpdateCommand(object obj)
		{
			return true;
		}
	}
}
