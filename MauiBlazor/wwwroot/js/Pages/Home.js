// キー入力を捕捉。
window.addEventListener('keydown', function (event) {
    DotNet.invokeMethodAsync('Marimo.WindowsCalculator.MauiBlazor', 'HandleKeyDown', event.key, event.ctrlKey);
});
//画面の大きさ変更を補足し、大きさに合った描画を行う。最初にも行う。
function setDivHeight() {
    const div = document.querySelector('.layout-root');
    div.style.height = `${document.documentElement.clientHeight}px`;
}

window.addEventListener('resize', setDivHeight);

const observer = new MutationObserver((mutations) => {
    setDivHeight();
});
observer.observe(document.body, { attributes: true, childList: true, subtree: true });