document.addEventListener('DOMContentLoaded', function () {
    var originInput = document.getElementById('originInput');
    var destinationInput = document.getElementById('destinationInput');
    var submitBtn = document.getElementById('submitBtn');
    var resultContainer = document.getElementById('result');

    submitBtn.addEventListener('click', function () {
        var requestData = {
            Slices: [{
                Origin: originInput.value,
                Destination: destinationInput.value
            }]
        };

        fetch('/api/plane/MakeApiCall', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestData)
        })
            .then(response => response.json())
            .then(data => {
                console.log(data)
            })
            .catch(error => {
                resultContainer.innerHTML = 'API Error: ' + error;
            });
    });
});
