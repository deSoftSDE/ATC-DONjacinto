// Write your JavaScript code.
function getAccordionInstance() {
    return $("#accordion-container").dxAccordion("instance");
}

function getTagBoxInstance() {
    return $("#tagbox").dxTagBox("instance");
}

function tagBox_valueChanged(e) {
    getAccordionInstance().option("selectedItemKeys", e.value);
}

function accordion_selectionChanged(e) {
    getTagBoxInstance().option("value", e.component.option("selectedItemKeys"));
}

function slider_valueChanged(e) {
    getAccordionInstance().option("animationDuration", e.value);
}

function multipleMode_changed(e) {
    var tagBox = getTagBoxInstance(),
        accordion = getAccordionInstance();

    accordion.option("multiple", e.value);
    tagBox.option("disabled", !e.value);
    tagBox.option("value", accordion.option("selectedItemKeys"));
}

function collapsibleMode_changed(e) {
    getAccordionInstance().option("collapsible", e.value);
}
