document.addEventListener('DOMContentLoaded', function () {
    var originInput = document.getElementById('originInput');
    var destinationInput = document.getElementById('destinationInput');
    var submitBtn = document.getElementById('submitBtn');
    var resultContainer = document.getElementById('result');

    function parseDuration(duration) {
        const matches = duration.match(/PT(\d+)H(\d+)M/);
        if (matches) {
            const hours = parseInt(matches[1]);
            const minutes = parseInt(matches[2]);
            return `${hours} hours and ${minutes} minutes`;
        }
        return 'Invalid duration format';
    }


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

                let offers = data.data.offers;

                let container = document.createElement('div');
                container.id = 'offer-container';

                offers.forEach(offer => {
                    let html = `
             <div class = "booking-list-item">
               <div class = "booking-list-item-inner">
                <div class="booking-list-top">
                                        <div class="flight-airway">
                                            <div class="flight-logo">
                                               
                                                <h5 class="title">${offer.owner.name}</h5>
                                            </div>
                                            <span>Operated by ${offer.owner.name}</span>
                                        </div>
                                        <ul class="flight-info">
                                            <li>${offer.slices[0].segments[0].departing_at}</li>
                                            <li class="time"><span>12: 55</span>DAC</li>
                                            <li>${parseDuration(offer.slices[0].duration)}<span>2 Stops</span></li>
                                        </ul>
                                        <div class="flight-price">
                                            <h4 class="title">GB£${offer.base_amount}</h4>
                                            <a href="booking-details.html" class="btn">Select <i class="flaticon-flight-1"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>


                 `;
                   container.insertAdjacentHTML('beforeend', html); // Append the HTML to the container
                })

                resultContainer.appendChild(container);

                console.log(data)
            })
            .catch(error => {
                resultContainer.innerHTML = 'API Error: ' + error;
            });
    });
});
