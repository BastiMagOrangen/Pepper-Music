let selectFile = document.getElementById('selectFile');
let file = null;

selectFile.addEventListener('change', (event) => {
    file = event.target.files[0];
    console.log(event.target.files);

    let audio = new Audio(file.path);
    audio.play();
});


