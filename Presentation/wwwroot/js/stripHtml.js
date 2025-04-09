
//Took help from ChatGpt to clean html - tags from Description and display pure text
function stripHtml(text) {
    const tempParagraf = document.createElement("p");
    tempParagraf.innerHTML = text;
    return tempParagraf.textContent || tempParagraf.innerText || "";
}

function initDescriptionStripHtml() {
    const descElement = document.getElementById("project-description");
    if (!descElement) return;

    const rawHtml = descElement.dataset.rawHtml;
    const cleanText = stripHtml(rawHtml);
    descElement.textContent = cleanText;
}