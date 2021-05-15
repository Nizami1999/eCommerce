var quick_view = document.getElementById("quick_view");
var appendModal = document.getElementById("appendModal");

var phoneDetails = () => {
    var smartphoneId = event.target.getAttribute('data-id');

    $.ajax({
        url: "/AJAX/Details",
        type: "GET",
        dataType: "json",
        data: { smartphoneId: smartphoneId },
        success: function (res) {
            console.log(res);
            quick_view.style.display = "block";

            var modal = `
                        <div class="row">
                            <div class="col-xs-12 col-sm-5 col-md-4">
                                <div class="quick-image">
                                    <div style="margin: 0 auto; height: 500px; overflow: hidden">
                                        <img style="object-fit: contain; width: 100%; height: 100%;" src="/Public/img/Smartphones/${res[0].Image}" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-7 col-md-8">
                                <div class="quick-right">
                                    <div class="list-text">
                                        <h3>${res[0].Name}</h3>
                                        <h5>$${res[0].NewPrice}</h5>
                                        <p>${res[0].Info}</p>

                                        <ul>
                                            <h4>Main Color : <span>${res[0].Color1}</span></h4>
                                            <h4>Memory : <span>${res[0].Memory}</span></h4>
                                            <h4>Camera : <span>${res[0].Camera}</span></h4>
                                        </ul>

                                        <div class="share-tag clearfix">
                                            <ul class="blog-share floatleft">
                                                <li><h5>share </h5></li>
                                                <li><a href="#"><i class="mdi mdi-facebook"></i></a></li>
                                                <li><a href="#"><i class="mdi mdi-twitter"></i></a></li>
                                                <li><a href="#"><i class="mdi mdi-linkedin"></i></a></li>
                                                <li><a href="#"><i class="mdi mdi-instagram"></i></a></li>
                                            </ul>
                                        </div>
                                        <div class="submit-text">
                                            <a href="/Home/Details/${res[0].Id}">Read more</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
               
            `;

            appendModal.innerHTML = modal;
        }
    });
}

var quick_viewClose = () => {
    appendModal.innerHTML = "";
    document.getElementById("quick_view").style.display = "none";
}

// Get Smartphone (AJAX)
var getSmartphone = () => {
    event.preventDefault();

    var smartphoneId = event.target.getAttribute('data-id');
    $.ajax({
        url: "/AJAX/AddSmartphone",
        type: "GET",
        dataType: "json",
        data: { smartphoneId: smartphoneId },
        success: function (res) {
            if (res.status == 200) {
                swal("Well done!", res.message, "success");
            }
            else {
                swal("Ops!", res.error, "error");
            }
        }
    });
}

// Remove Smartphone (AJAX)
var removeBook = () => {
    event.preventDefault();

    var smartphoneId = event.target.getAttribute('data-id');
    $.ajax({
        url: "/AJAX/RemoveBook",
        type: "GET",
        dataType: "json",
        data: { smartphoneId: smartphoneId },
        success: function (res) {
            if (res.status == 200) {

                swal({
                    title: "Removed!",
                    text: res.message,
                    type: "success"
                }).then(function () {
                    window.location.reload();
                });
            }
            else {
                swal("Ops!", res.error, "error");
            }
        }
    });
}

// Search smartphone (AJAX)
var searchSmartphone = () => {

    document.getElementById("smartphone-group").innerHTML = null;

    if (document.getElementById("searchSmartphone").value) {
        $.ajax({
            url: "/AJAX/SearchSmartphone",
            data: { value: document.getElementById("searchSmartphone").value },
            dataType: "json",
            type: "GET",
            success: function (res) {
                if (res.length) {
                    for (var s of res) {

                        var tr = `
                            <tr style="background-color: white;">
                                <td class="td-img text-left">
                                    <a href="/Home/Details/${s.Id}">
                                        <div style="margin: 1rem 0 0 0; background-image: url(/Public/img/Smartphones/${s.Image}); background-repeat: no-repeat; background-position: center; background-size: contain; height: 70px"></div>
                                    </a>
                                    <div class="items-dsc">
                                        <h5><a href="/Home/Details/${s.Id}">${s.Name}</a></h5>
                                        <p class="itemcolor">Brand   : <span>${s.Brand}</span></p>
                                        <p class="itemcolor">Memory   : <span>${s.Memory}</span></p>
                                    </div>
                                </td>
                                <td>$${s.NewPrice}</td>
                                <td>In Stock</td>
                                <td>
                                    <div class="submit-text">
                                        <a href="/Home/Details/${s.Id}">Read more</a>
                                    </div>
                                </td>
                            </tr>
                        `
                        document.getElementById("smartphone-group").innerHTML += tr;
                    }
                }
                else {
                    var tr = `
                            <tr >
                                <td>No such product</td>
                                <td>No such product</td>
                                <td>No such product</td>
                                <td>No such product</td>
                            </tr>
                        `
                    document.getElementById("smartphone-group").innerHTML += tr;
                }

            },
            error: function (res, status, error) {
                console.log(error);
            }
        });
    }

}