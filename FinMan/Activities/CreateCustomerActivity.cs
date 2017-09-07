
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using FinMan.Data;


namespace FinMan
{
    [Activity]
    public class CreateCustomerActivity : Activity
    {
        private CustomerClient customerClient;
        bool _isCustomerUpdate;
        string _personalNumber;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CreateCustomer);

            _isCustomerUpdate = Intent.Extras.GetBoolean("CustomerUpdate", false);
            _personalNumber = Intent.Extras.GetString("PersonalNumber", "");

            Button buttonCreateCustomer = FindViewById<Button>(Resource.Id.createCustomerButton);
            Button buttonUpdateCustomer = FindViewById<Button>(Resource.Id.updateCustomerButton);

            customerClient = new CustomerClient();

            if (_isCustomerUpdate && !string.IsNullOrEmpty(_personalNumber))
            {
                GetCustomerDetails(_personalNumber);

                buttonCreateCustomer.Visibility = ViewStates.Gone;
                buttonUpdateCustomer.Visibility = ViewStates.Visible;
                buttonUpdateCustomer.Click += UpdateCustomer;
            }
            else
            {
                buttonCreateCustomer.Visibility = ViewStates.Visible;
                buttonUpdateCustomer.Visibility = ViewStates.Gone;
                buttonCreateCustomer.Click += CreateNewCustomer;
            }
        }

        async void GetCustomerDetails(string personalNumber)
        {
            CustomerResponse customerDetails = await customerClient.GetCustomerAccountDetails(personalNumber);
            //FindViewById<EditText>(Resource.Id.customerNumberTextbox).Text = customerDetails.personalNumber;
            FindViewById<EditText>(Resource.Id.firstNameTextbox).Text = customerDetails.firstName;
            FindViewById<EditText>(Resource.Id.lastNameTextbox).Text = customerDetails.lastName;
            FindViewById<EditText>(Resource.Id.dobTextbox).Text = customerDetails.dateOfBirth;

            if (customerDetails.gender.ToLower().Equals("male"))
                (FindViewById<CheckBox>(Resource.Id.maleCheckbox)).Checked = true;
            else if (customerDetails.gender.ToLower().Equals("female"))
                (FindViewById<CheckBox>(Resource.Id.femaleCheckbox)).Checked = true;

            FindViewById<EditText>(Resource.Id.nationalityTextbox).Text = customerDetails.nationality;

            FindViewById<EditText>(Resource.Id.addressStreetTextbox).Text = customerDetails.address.street;
            FindViewById<EditText>(Resource.Id.addressPostalCodeTextbox).Text = customerDetails.address.postalCode;
            FindViewById<EditText>(Resource.Id.cityTextbox).Text = customerDetails.address.city;
            FindViewById<EditText>(Resource.Id.countryTextbox).Text = customerDetails.address.country;

            FindViewById<EditText>(Resource.Id.phoneNumberTextbox).Text = customerDetails.phoneNumber;
            FindViewById<EditText>(Resource.Id.emailidTextbox).Text = customerDetails.email;
            FindViewById<EditText>(Resource.Id.passportNumberTextbox).Text = customerDetails.idNumber;
        }

        async void CreateNewCustomer(object sender, EventArgs e)
        {
            var newCustomer = SetCustomerObject();
            CustomerResponse customerResponse = await customerClient.CreateCustomer(newCustomer);

            //if(!string.IsNullOrEmpty(customerResponse.customerID))
            //redirect to page to create an account
        }

        private Customer SetCustomerObject()
        {
            Customer customer = new Customer();

            customer.personalNumber = _personalNumber;//FindViewById<EditText>(Resource.Id.customerNumberTextbox).Text;
            customer.firstName = FindViewById<EditText>(Resource.Id.firstNameTextbox).Text;
            customer.lastName = FindViewById<EditText>(Resource.Id.lastNameTextbox).Text;
            customer.dateOfBirth = FindViewById<EditText>(Resource.Id.dobTextbox).Text;

            var genderMale = (FindViewById<CheckBox>(Resource.Id.maleCheckbox)).Checked;
            var genderFemale = (FindViewById<CheckBox>(Resource.Id.femaleCheckbox)).Checked;
            customer.gender = (genderMale ? "Male" : (genderFemale ? "Female" : ""));

            customer.nationality = FindViewById<EditText>(Resource.Id.nationalityTextbox).Text;

            Address address = new Address();
            address.street = FindViewById<EditText>(Resource.Id.addressStreetTextbox).Text;
            address.postalCode = FindViewById<EditText>(Resource.Id.addressPostalCodeTextbox).Text;
            address.city = FindViewById<EditText>(Resource.Id.cityTextbox).Text;
            address.country = FindViewById<EditText>(Resource.Id.countryTextbox).Text;
            customer.address = address;

            customer.phoneNumber = FindViewById<EditText>(Resource.Id.phoneNumberTextbox).Text;
            customer.email = FindViewById<EditText>(Resource.Id.emailidTextbox).Text;
            customer.idNumber = FindViewById<EditText>(Resource.Id.passportNumberTextbox).Text;
            customer.idType = "Passport";

            return customer;
        }

        async void UpdateCustomer(object sender, EventArgs e)
        {
            var updatedCustomer = SetCustomerObject();
            UpdateCustomerRequestResponse updateCustomerResponse = await customerClient.UpdateCustomer(updatedCustomer.personalNumber, updatedCustomer.phoneNumber, updatedCustomer.email, updatedCustomer.address);

            //if(updateCustomerResponse == null)
            //error page
        }
    }
}
