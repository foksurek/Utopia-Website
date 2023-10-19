
var savedData = [];
var SearchsavedData = [];
var bgcheck = 0;
var resultsPerPage = 20;
var offset = 0;
var temp = "1";
var term;
var termOffset;
let selectedOptions = "1";

$(function () {
    function searchMaps() {
        $.ajax({
            url: 'sql/getMaps',
            dataType: "json",
            data: { term: '', offset: offset, limit: resultsPerPage, status: selectedOptions },
            success: function (data) {
                savedData = data.data;
                displayResults(savedData);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $(".map-search-results").empty();
                $("body").append(`<div class="error-notification"><span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>Cant connect to database. Please try again later</div>`);
            }
        });
    }

    function displayResults(data) {
        $(".map-search-results").empty();
        for (i in data) {
            switch(data[i].rankedStatus) {
                case -2:
                    status = 'GRAVEYARD';
                    break;
                case -1:
                    status = 'WIP';
                    break;
                case 0:
                    status = 'PENDING';
                    break;
                case 1:
                    status = 'RANKED';
                    break;
                case 2:
                    status = 'APPROVED';
                    break;
                case 3:
                    status = 'QUALIFIED';
                    break;
                case 4:
                    status = 'LOVED';
                    break;
                default:
                    status = 'UNKNOWN';
                    break;
            }
            $(".map-search-results").append(`
            <a href="/beatmaps/${data[i].ids[0]}">
                <div class="map" style="--bg:url('https://assets.ppy.sh/beatmaps/${data[i].setID}/covers/cover.jpg')">
                    <div class="map-content">
                        <div class="status">
                            <span class="map-status">${status}</span>
                        </div>
                        <div class="header">
                            <span class="map-creator">${data[i].creator}</span>
                        </div>
                        <div class="title">
                            <span class="map-title">${data[i].title}</span>
                            <span class="map-artist">${data[i].artist}</span>
                        </div>
                    </div>
                </div>
            </a>
        `);
        }
    }

    searchMaps();

    function rserach() {
        $.ajax({
            url: 'sql/getMaps',
            dataType: "json",
            data: { term: request.term, limit: 20, status: selectedOptions },
            success: function (data) {
                var sData = data.data;
                displayResults(sData);
                temp = "0";
                termOffset = 0;
                term = request.term;
                SearchsavedData = data.data;

            }
        });
    }

    $("#searchMap").autocomplete({
        source: function (request, response) {
            if ($("#searchMap").val() != '') {
                $.ajax({
                    url: 'sql/getMaps',
                    dataType: "json",
                    data: { term: request.term, limit: 20, status: selectedOptions },
                    success: function (data) {
                        var sData = data.data;
                        displayResults(sData);
                        temp = "0";
                        termOffset = 0;
                        term = request.term;
                        SearchsavedData = data.data;

                    }
                });
            } else {
                displayResults(savedData);
                temp = "1";
            }

        },
        minLength: 0,
    }).focus(function () {
        if ($("#searchMap").val() != '') {
            $(this).autocomplete("search");
        } else {
            temp = "1";
            displayResults(savedData);
        }
    });
    $(window).scroll(function (request) {
        if ($(window).scrollTop() + $(window).height() == $(document).height()) {
            if (temp == "1") {
                var offset = savedData.length; // Obliczenie wartości offset
                $.ajax({
                    url: 'sql/getMaps',
                    dataType: "json",
                    data: { term: '', offset: offset, status: selectedOptions},
                    success: function (data) {
                        savedData = savedData.concat(data.data);
                        displayResults(savedData);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        // Obsługa błędu
                    }
                });
            }
            else {
                var offset = termOffset; // Obliczenie wartości offset
                termOffset += 20;
                $.ajax({
                    url: 'sql/getMaps',
                    dataType: "json",
                    data: { term: term, offset: offset, limit: 20, status: selectedOptions},
                    success: function (data) {
                        SearchsavedData = SearchsavedData.concat(data.data);
                        displayResults(SearchsavedData);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        // Obsługa błędu
                    }
                });
            }
        }
    });
    const statusPanel = document.querySelector('.SelectStatusPanel');
    const so = ['1'];

    statusPanel.addEventListener('click', (event) => {
        const option = event.target.getAttribute('data-option');

        if (option) {
            const optionIndex = so.indexOf(option);

            if (optionIndex === -1) {
                so.push(option);
                event.target.classList.add('active');
            } else {
                so.splice(optionIndex, 1);
                event.target.classList.remove('active');
            }

            // update display
            selectedOptions = so.join(', ');
            console.log(selectedOptions);
        }
        if ($("#searchMap").val() != '') {
            $.ajax({
                url: 'sql/getMaps',
                dataType: "json",
                data: { term: term, limit: 20, status: selectedOptions },
                success: function (data) {
                    var sData = data.data;
                    displayResults(sData);
                    temp = "0";
                    termOffset = 20;
                    SearchsavedData = data.data;
                }
            });
        } else {
            searchMaps()
            temp = "1";
        }
    });
});