window.addHideMethodTo = (element, idNames) ->
  element[0].hideFieldSet = (result) ->
    fieldSet = jQuery(element).find("##{idNames.fieldSetName}")
    fieldSet.hide()


window.createHandleDescriptionSaveMethod = (fieldSet, idNames, setStateToView) ->
  handleDescriptionSave = (result) ->
    fieldSet.find("##{idNames.divDescriptionTextName}").text(result)
    setStateToView(fieldSet, idNames)
    undefined


window.setButtonClicks = (element, idNames, proto, editDescriptionMethod) ->
  fieldSet = jQuery(element).find("##{idNames.fieldSetName}")

  fieldSet.find("##{idNames.buttonEditDescriptionName}").click ->
    window.setStateToEdit fieldSet, idNames

  fieldSet.find("##{idNames.buttonSaveDescriptionName}").click ->
    id = fieldSet.find("##{idNames.hiddenIdName}").val()
    text = fieldSet.find("##{idNames.textareaEditDescriptionName}").val()
    editDescriptionMethod id, text, window.createHandleDescriptionSaveMethod(fieldSet, idNames, window.setStateToView)
    undefined


window.setStateToEdit = (element, idNames) ->
  jQuery(element).find("##{idNames.buttonEditDescriptionName}").hide()
  jQuery(element).find("##{idNames.divDescriptionTextName}").hide()
  jQuery(element).find("##{idNames.buttonSaveDescriptionName}").show()
  jQuery(element).find("##{idNames.divEditDescriptionTextName}").show()
  undefined


window.setStateToView = (element, idNames) ->
  jQuery(element).find("##{idNames.buttonEditDescriptionName}").show()
  jQuery(element).find("##{idNames.divDescriptionTextName}").show()
  jQuery(element).find("##{idNames.buttonSaveDescriptionName}").hide()
  jQuery(element).find("##{idNames.divEditDescriptionTextName}").hide()
  undefined
