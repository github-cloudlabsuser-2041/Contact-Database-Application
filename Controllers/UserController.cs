using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        public ActionResult Search(string searchString)
        {
            // Convert the search string to lower case for case insensitive search
            searchString = searchString?.ToLower();

            // Find the users that match the search string
            var matchingUsers = userlist.Where(u => u.Name.ToLower().Contains(searchString) || u.Email.ToLower().Contains(searchString));

            // Pass the matching users to the Search view
            return View(matchingUsers);
        }


        // GET: User
        public ActionResult Index()
        {
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Implement the details method here
            // Retrieve the user from the userlist based on the provided ID
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // Check if the user exists
            if (user == null)
            {
                return HttpNotFound();
            }

            // Pass the user to the Details view
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // This method is responsible for displaying the view to create a new user.
            // It returns the Create view, which contains a form for the user to input their information.
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Validate the user input
                // For example, you can check if the required fields are filled

                // Create a new User object
                User newUser = new User
                {
                    // Set the properties of the new user based on the input
                    // For example, you can assign the values from the user parameter to the corresponding properties of the newUser object
                    Id = userlist.Count + 1, // Generate a new ID based on the number of existing users
                    Name = user.Name,
                    Email = user.Email,
                    // Assign other properties as needed
                };

                // Add the new user to the userlist
                userlist.Add(newUser);

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }

            // If there are validation errors, return the Create view to display the errors
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the user from the userlist based on the provided ID
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // Check if the user exists
            if (user == null)
            {
                return HttpNotFound();
            }

            // Pass the user to the Edit view
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User updatedUser)
        {
            // Retrieve the user from the userlist based on the provided ID
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // Check if the user exists
            if (user == null)
            {
                return HttpNotFound();
            }

            // Update the properties of the existing user with the values from the updatedUser parameter
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            // Update other properties as needed

            // Redirect to the Details action to display the updated user details
            return RedirectToAction("Details", new { id = user.Id });
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the user from the userlist based on the provided ID
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // Check if the user exists
            if (user == null)
            {
                return HttpNotFound();
            }

            // Pass the user to the Delete view
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Retrieve the user from the userlist based on the provided ID
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // Check if the user exists
            if (user == null)
            {
                return HttpNotFound();
            }

            // Remove the user from the userlist
            userlist.Remove(user);

            // Redirect to the Index action to display the updated list of users
            return RedirectToAction("Index");
        }
    }
}
