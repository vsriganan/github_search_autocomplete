// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ajaxStart(function () {
//    // Show image container
//    $("#buttonloader").show();
//});
//$(document).ajaxComplete(function () {
//    // Hide image container
//    $("#buttonloader").hide();
//});

$(document).ready(function () {
    /*$("#buttonloader").hide(); //Hide the Loader*/
    $("#search-results-container").hide();
});

//#region Button Click Events

$('#search').click(function () {
    mainSearch();
});

$('#clear').click(function () {
    clearContainer();
});

$('#searchText').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        mainSearch();
    }
});

$('#repository-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/Repository?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/Repository")
});

$('#code-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/Code?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/Code")
});

$('#commit-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/Commit?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/Commit")
});

$('#issue-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/Issue?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/Issue")
});

$('#topic-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/Topic?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/Topic")
});

$('#user-li').click(function (e) {
    ajaxGET(e, 'https://localhost:44310/Search/User?searchTerm=' + $("#searchText").val() + '&perPage=10&pageNumber=1', "/Home/User")
});

//#endregion

//#region JS Functions
function resetCounts() {
    $("#repository-count").text("0");
    $("#code-count").text("0");
    $("#commit-count").text("0");
    $("#issue-count").text("0");
    $("#topic-count").text("0");
    $("#user-count").text("0");
}

function setActive(e) {
    if (document.querySelector('#ul-list li.active') !== null) {
        document.querySelector('#ul-list li.active').classList.remove('active');
    }
    e.target.className += " active";
}

function ajaxGET(e, ajaxURL, mvcView) {
    if ($("#searchText").val() == "")
    {
        alert("Enter a Search Term!");
    }
    else {
        setActive(e);

        $.ajax({
            url: ajaxURL,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                $("#view_display").load(mvcView, data);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error occured!");
            }
        });
    }    
}

function mainSearch() {
    if ($("#searchText").val() == "") {
        alert("Enter a Search Term!");
    }
    else {
        $.ajax({
            url: 'https://localhost:44310/Search/All?searchTerm=' + $("#searchText").val(),
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                if (data == null) {
                    alert("Your search did not return any result!");
                    clearContainer();
                }
                else {
                    $("#repository-count").text(data.repository_search.total_count);
                    $("#code-count").text(data.code_search.total_count);
                    $("#commit-count").text(data.commit_search.total_count);
                    $("#issue-count").text(data.issue_search.total_count);
                    $("#topic-count").text(data.topic_search.total_count);
                    $("#user-count").text(data.user_search.total_count);

                    //Display the Search Result container
                    $("#search-results-container").show();

                    //Trigger the first Repository Search
                    $('#ul-list li:nth-child(1)').click();

                    //Disable Search Text
                    $('#searchText').attr('disabled', 'disabled');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("The API returned an Error! Please try again after sometime!");              
                clearContainer();
            }
        });
    }    
}

function clearContainer() {
    resetCounts(); //OnError set counter text to zero
    $("#view_display").load('', '');
    $("#search-results-container").hide();

    //Enable Search Text
    $('#searchText').removeAttr('disabled');
    $('#searchText').val('');
}
//#endregion