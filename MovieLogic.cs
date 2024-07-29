using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module4
{
	// Movie Management System class
	public class MovieLogic : IMovie
	{
		public ObservableCollection<Movie> moviesCollection; // Using linked list to store movies
		private Stack<string> actionHistory; // Using stack to maintain action history
		private Dictionary<int, Movie> movieDictionary; // Using dictionary for fast movie lookup by ID

		// Constructor
		public MovieLogic()
		{
			moviesCollection = new ObservableCollection<Movie>();
			actionHistory = new Stack<string>();
			movieDictionary = new Dictionary<int, Movie>();
		}

		// Method to add a movie
		public void AddMovie(Movie movie)
		{
			moviesCollection.Add(movie);
			actionHistory.Push($"Added movie: {movie.Title}");
			movieDictionary.Add(movie.MovieID, movie);
		}

		// Method to view all movies
		public void ViewAllMovies()
		{
			foreach (var movie in moviesCollection)
			{
				movie.DisplayDetails();
				Console.WriteLine();
			}
		}

		// Method to update a movie
		public void UpdateMovie(Movie movie)
		{
			Movie movieToUpdate = new Movie(); 
			if (movie!=null)
			{
				movieToUpdate.Title = movie.Title;
				movieToUpdate.Genre = movie.Genre;
				movieToUpdate.ReleaseYear = movie.ReleaseYear;
				movieToUpdate.Description = movie.Description;
				movieToUpdate.Duration = movie.Duration;
				movieToUpdate.Rating = movie.Rating;
				movieToUpdate.Director = movie.Director;
				movieToUpdate.Cast = movie.Cast;
				movieToUpdate.IsAvailable = movie.IsAvailable;
				actionHistory.Push($"Updated movie: {movie.Title}");
			}
			else
			{
				Console.WriteLine("Movie not found.");
			}
		}

		// Method to delete a movie
		public void DeleteMovie(int movieID)
		{
			Movie movieToDelete;
			if (movieDictionary.TryGetValue(movieID, out movieToDelete))
			{
				moviesCollection.Remove(movieToDelete);
				actionHistory.Push($"Deleted movie: {movieToDelete.Title}");
				movieDictionary.Remove(movieID);
				Console.WriteLine("Movie deleted successfully.");
			}
			else
			{
				Console.WriteLine("Movie not found.");
			}
		}

		// Method to search for a movie by title
		public void SearchMovieByTitle(string title)
		{
			Movie movie = moviesCollection.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
			if (movie != null)
			{
				movie.DisplayDetails();
			}
			else
			{
				Console.WriteLine("Movie not found.");
			}
		}

		// Method to search for movies by genre
		public void SearchMoviesByGenre(Genre genre)
		{
			var matchingMovies = moviesCollection.Where(m => m.Genre == genre);
			if (matchingMovies != null && matchingMovies.Any())
			{
				foreach (var movie in matchingMovies)
				{
					movie.DisplayDetails();
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("No movies found for the specified genre.");
			}
		}

		// Method to display action history
		public void DisplayActionHistory()
		{
			Console.WriteLine("Action History:");
			foreach (var action in actionHistory)
			{
				Console.WriteLine(action);
			}
		}
	}
}
