class window.tableDescriptionContainer

  constructor: (thePassedInElement, columnDescriptionMethod, editTableDescriptionMethod) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")


    proto = tableDescriptionContainer.prototype

    idBag =
      buttonEditDescriptionName: "buttonEditTableDecription"
      buttonSaveDescriptionName: "buttonSaveTableDescription"
      columnButtonPrefix: "buttonColumnItem_"
      divColumnButtonHolderName: "divColumnButtonHolder"
      divDescriptionTextName: "divDescriptionText"
      divEditDescriptionTextName: "divEditDescriptionText"
      fieldSetName: "fieldsetTableDescriptionText"
      hiddenIdName: "hiddenTableId"
      legendName: "legend"
      tableHeaderName: "tableDescriptionHeader"
      textareaEditDescriptionName: "textareaEditDescription"


    thePassedInElement = addTo thePassedInElement, proto.tableDescriptionContainerCreator, {idNames: idBag}
    proto.addUpdateDescriptionMethodTo thePassedInElement, proto.createSetColumnButtonsMethod(idBag, proto), columnDescriptionMethod, window.setStateToView, idBag
    window.addHideMethodTo thePassedInElement, idBag
    window.setButtonClicks thePassedInElement, idBag, proto, editTableDescriptionMethod

    thePassedInElement


  ### Table Description Area Methods ###

  addUpdateDescriptionMethodTo : (element, createButtons, columnDescriptionMethod, setStateToView, idNames) ->
    element[0].updateDescription = (result) ->

      ##{result.DatabaseName}
      ## {result.SchemaName}

      description = if result.Description? then result.Description else ""

      fieldSet = jQuery(element).find("##{idNames.fieldSetName}")
      fieldSet.find("##{idNames.divDescriptionTextName}").text("#{description}")
      fieldSet.find("##{idNames.tableHeaderName}").text("[#{result.DatabaseName}.#{result.SchemaName}]")
      fieldSet.find("##{idNames.legendName}").text("#{result.TableName}")
      fieldSet.find("##{idNames.textareaEditDescriptionName}").val(description)
      fieldSet.find("##{idNames.hiddenIdName}").val(result.Id)

      createButtons fieldSet, result.ColumnList, columnDescriptionMethod
      setStateToView fieldSet, idNames

      fieldSet.show()
      undefined

  tableDescriptionContainerCreator: ->
    fieldset id: @idNames.fieldSetName, class: @idNames.fieldSetName, style: "display:none;", ->
      legend id: @idNames.legendName, ->
        ""
      div id: @idNames.tableHeaderName, class: @idNames.tableHeaderName, ->
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
      div id: @idNames.divColumnButtonHolderName, ->
        ""
      div ->
        input id:@idNames.hiddenIdName, type:"hidden"


  ### Column Button Methods ###

  columnButtonCreator: ->
    for item in @columnList
      button id:"#{@itemPrefix}#{item.Id}", class: "columnNameButton", ->
        "#{item.ColumnName}"


  createSetColumnButtonsMethod: (idNames, proto) ->
    setColumnButtons = (fieldSet, itemList, columnDescriptionMethod) ->
      buttonHolder = fieldSet.find("##{idNames.divColumnButtonHolderName}")
      buttonHolder.children().remove()

      addTo buttonHolder, proto.columnButtonCreator, {itemPrefix : idNames.columnButtonPrefix, columnList: itemList}

      for item in itemList
        setClickForElement(buttonHolder, idNames.columnButtonPrefix, item, columnDescriptionMethod)

      undefined


### End Column Button Methods ###
