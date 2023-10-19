let type;

function selectImage() {
    var input = document.getElementById('imageFile');
    input.value = null;
    type = "avatar";
    input.click();
}
function selectBanner() {
    var input = document.getElementById('imageFile');
    input.value = null;
    type = "banner";
    input.click();
}



document.getElementById('imageFile').addEventListener('change', function() {
    uploadImage();
    if (type === "avatar") {
        document.getElementById('avatar-settings-img').style.opacity = 0.5;
    } else {
        document.getElementById('banner-settings-img').style.opacity = 0.5;
    }
});

function uploadImage() {
    let input = document.getElementById('imageFile');
    let file = input.files[0];

    if (file) {
        let formData = new FormData();
        formData.append('imageFile', file);
        

        fetch(`/File/SaveImage?type=${type}`, {
            method: 'POST',
            body: formData
        })
            .then(response => {
                if (response.status === 422) {
                    document.querySelector('.file-upload-error').style.display = 'block';
                }
                if (response.status === 200) {
                    location.reload();
                }
                document.getElementById('avatar-settings-img').style.opacity = 1;
                document.getElementById('banner-settings-img').style.opacity = 1;
            })
            .catch(error => {
                document.getElementById('avatar-settings-img').style.opacity = 1;
                document.getElementById('banner-settings-img').style.opacity = 1;
                console.error(error);
            })
    }
}