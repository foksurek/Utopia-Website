const loginModal = document.querySelector('.user-modal');
const loginElement = document.querySelector('.login-ul-element');
let searchTimeout;

function showModal() {
    loginModal.style.display = 'block';
}

function hideModal() {
    loginModal.style.display = 'none';
}

loginElement.addEventListener('click', showModal);

document.addEventListener('click', (event) => {
    if (!loginModal.contains(event.target) && !loginElement.contains(event.target)) {
        hideModal();
    }
});

document.addEventListener('DOMContentLoaded', function () {
    const searchTerm = document.getElementById('searchTerm');
    const searchResults = document.querySelector('.search-results');

    searchTerm.addEventListener('input', function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(function () {
            const request = new XMLHttpRequest();
            request.onreadystatechange = function () {
                if (request.readyState === XMLHttpRequest.DONE) {
                    if (request.status === 200) {
                        searchResults.innerHTML = '';
                        const data = JSON.parse(request.responseText);
                        const asset = data;
                        let temp = 1;
                        for (const i in asset.data) {
                            searchResults.insertAdjacentHTML(
                                'beforeend',
                                `<a class='player' href='/u/${asset.data[i].id}'>
                  <div class='background' style='--bg:url("/getAvatar/${asset.data[i].id}b")'></div>
                  <div class='meta'>
                    <img src='/getavatar/${asset.data[i].id}' alt='2'>
                    <div class='info'>
                      <div class='flag'></div>
                      <h1>${asset.data[i].name}</h1>
                    </div>
                  </div>
                </a>`
                            );
                            temp++;
                            if (temp > 5) break;
                        }
                    }
                }
            };
            const url = '/Api/getSuggestions?term=' + encodeURIComponent(searchTerm.value);
            request.open('GET', url, true);
            request.send();
        }, 500); // Opóźnienie czasowe: 500 milisekund (0.5 sekundy)
    });

    searchTerm.addEventListener('focus', function () {
        searchTerm.dispatchEvent(new Event('input'));
    });
});