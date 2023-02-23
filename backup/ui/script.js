let selectFile = document.getElementById('selectFile');
let nextButton = document.getElementById('next');
//let file = null;
let audio = new Audio();
let fileList = [];
let count = 0;

nextButton.addEventListener('click', () => {
    //audio.stop()
    audio.pause();
    audio.currentTime = 0;
    count++;
    audio = new Audio(fileList[count].path);
    audio.addEventListener("timeupdate", (event) => {
        console.log(audio.currentTime);
    })
    audio.play();
});

selectFile.addEventListener('change', (event) => {
    //event.target.files.forEach(add);
    //fileList.concat(event.target.files);
    //file = event.target.files[0];
    for(var i = 0; i < event.target.files.length; i++)
    {
        fileList.push(event.target.files[i]);
    }
    console.log(event.target.files);
    console.log(fileList);
    count = 0;

    audio = new Audio(fileList[count].path);
    audio.play();
});


