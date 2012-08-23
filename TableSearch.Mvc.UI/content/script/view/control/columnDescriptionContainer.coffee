class window.columnDescriptionContainer

  constructor: (thePassedInElement, editColumnDescriptionMethod) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")

    idBag =
      buttonEditDescriptionName: "buttonEditColumnDecription"
      buttonSaveDescriptionName: "buttonSaveColumnDescription"
      divDescriptionTextName: "divDescriptionText"
      divDataTypeName: "divDataType"
      divEditDescriptionTextName: "divEditColumnDescriptionTextName"
      fieldSetName: "fieldsetColumnDescriptionText"
      hiddenIdName: "hiddenColumnIdName"
      legendName: "legend"
      textareaEditDescriptionName: "textareaEditColumnDescriptionName"

    proto = columnDescriptionContainer.prototype
    thePassedInElement = addTo thePassedInElement, proto.columnDescriptionContainerCreator, {idNames: idBag}
    proto.addUpdateDescriptionMethodTo thePassedInElement, idBag
    window.addHideMethodTo thePassedInElement, idBag
    window.setButtonClicks thePassedInElement, idBag, proto, editColumnDescriptionMethod

  ### Description ###

  addUpdateDescriptionMethodTo : (element, idNames) ->
    element[0].updateDescription = (result) ->

      description = if result.Description? then result.Description else ""


      fieldSet = jQuery(element).find("##{idNames.fieldSetName}")
      fieldSet.find("##{idNames.divDescriptionTextName}").text(description )
      fieldSet.find("##{idNames.textareaEditDescriptionName}").val(description)
      fieldSet.find("##{idNames.legendName}").text(result.ColumnName)
      fieldSet.find("##{idNames.divDataTypeName}").text("Data Type: #{result.DataType}")
      fieldSet.find("##{idNames.hiddenIdName}").val(result.ColumnId)

      setStateToView fieldSet, idNames

      fieldSet.show()
      undefined

  ### End Description ###

  columnDescriptionContainerCreator: ->
    fieldset id: @idNames.fieldSetName, class: @idNames.fieldSetName, style: "display:none;", ->
      legend id: @idNames.legendName, ->
        ""
      div id: @idNames.divDescriptionTextName, class: @idNames.divDescriptionTextName, ->
        ""
      div id: @idNames.divEditDescriptionTextName, style: "display:none;", ->
        textarea id: @idNames.textareaEditDescriptionName
      div ->
        button id: @idNames.buttonEditDescriptionName, type: "button", ->
          "edit"
        button id: @idNames.buttonSaveDescriptionName, type: "button", style: "display:none;", ->
          "save"
      div id: @idNames.divDataTypeName, class: @idNames.divDataTypeName, ->
        ""
      div ->
        input type: "hidden", id:@idNames.hiddenIdName
