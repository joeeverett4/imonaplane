// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.onload = function () {

    async function fetchData() {
        try {
            const response = await fetch('/api/plane'); // Change the URL to match your API endpoint
            if (!response.ok) {
                throw new Error('Failed to fetch data from the API.');
            }

            const data = await response.json();
            return data;
        } catch (error) {
            console.error(error);
            throw error;
        }
    }

    // Call the fetchData function and handle the data or error
    /*
    fetchData()
        .then((data) => {
            console.log('Data from API:', data);
            // Handle the data as needed
        })
        .catch((error) => {
            console.error('An error occurred:', error);
            // Handle the error as needed
        }); 
     */

    const fetchButton = document.getElementById("fetchButton");
    fetchButton.addEventListener("click", async () => {
        try {
            const data = await fetchData();
            console.log("Data from API:", data);
            // Handle the data as needed
        } catch (error) {
            console.error("An error occurred:", error);
            // Handle the error as needed
        }
    });

}