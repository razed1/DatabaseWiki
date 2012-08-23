(function() {
  window.addHideMethodTo = function(element, idNames) {
    return element[0].hideFieldSet = function(result) {
      var fieldSet;
      fieldSet = jQuery(element).find("#" + idNames.fieldSetName);
      return fieldSet.hide();
    };
  };
  window.createHandleDescriptionSaveMethod = function(fieldSet, idNames, setStateToView) {
    var handleDescriptionSave;
    return handleDescriptionSave = function(result) {
      fieldSet.find("#" + idNames.divDescriptionTextName).text(result);
      setStateToView(fieldSet, idNames);
      return;
    };
  };
  window.setButtonClicks = function(element, idNames, proto, editDescriptionMethod) {
    var fieldSet;
    fieldSet = jQuery(element).find("#" + idNames.fieldSetName);
    fieldSet.find("#" + idNames.buttonEditDescriptionName).click(function() {
      return window.setStateToEdit(fieldSet, idNames);
    });
    return fieldSet.find("#" + idNames.buttonSaveDescriptionName).click(function() {
      var id, text;
      id = fieldSet.find("#" + idNames.hiddenIdName).val();
      text = fieldSet.find("#" + idNames.textareaEditDescriptionName).val();
      editDescriptionMethod(id, text, window.createHandleDescriptionSaveMethod(fieldSet, idNames, window.setStateToView));
      return;
    });
  };
  window.setStateToEdit = function(element, idNames) {
    jQuery(element).find("#" + idNames.buttonEditDescriptionName).hide();
    jQuery(element).find("#" + idNames.divDescriptionTextName).hide();
    jQuery(element).find("#" + idNames.buttonSaveDescriptionName).show();
    jQuery(element).find("#" + idNames.divEditDescriptionTextName).show();
    return;
  };
  window.setStateToView = function(element, idNames) {
    jQuery(element).find("#" + idNames.buttonEditDescriptionName).show();
    jQuery(element).find("#" + idNames.divDescriptionTextName).show();
    jQuery(element).find("#" + idNames.buttonSaveDescriptionName).hide();
    jQuery(element).find("#" + idNames.divEditDescriptionTextName).hide();
    return;
  };
}).call(this);
