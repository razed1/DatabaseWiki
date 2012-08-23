class window.tableDescriptionContainer

  constructor: (thePassedInElement, columnDescriptionMethod) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")

    proto = tableDescriptionContainer.prototype
    thePassedInElement = addTo thePassedInElement, proto.tableDescriptionContainerCreator, {}
    proto.addUpdateDescriptionMethodTo thePassedInElement, proto.setColumnButtons, columnDescriptionMethod

    thePassedInElement

  ### Table Description Area Methods ###

  addUpdateDescriptionMethodTo : (element, createButtons, columnDescriptionMethod) ->
    element[0].updateDescription = (result) ->
      fieldSet = jQuery(element).find(".fieldsetTableDescriptionText")
      fieldSet.find(".divDescriptionText").text(result.Description)
      fieldSet.find("#legend").text(result.TableName)

      createButtons fieldSet, result.ColumnList, columnDescriptionMethod

      fieldSet.show()
      undefined


  tableDescriptionContainerCreator: ->
    fieldset class: "fieldsetTableDescriptionText", style: "display:none;", ->
      legend id: "legend", ->
        ""
      div class: "divDescriptionText", ->
        ""
      div id: "divColumnButtonHolder", ->
        ""


  ### Column Button Methods ###

  setColumnButtons: (fieldSet, itemList, columnDescriptionMethod) ->
    prefix = "buttonColumnItem_"
    proto = tableDescriptionContainer.prototype
    buttonHolder = fieldSet.find("#divColumnButtonHolder")
    buttonHolder.children().remove()

    addTo buttonHolder, proto.columnButtonCreator, {itemPrefix : prefix, columnList: itemList}

    for item in itemList
      setClickForElement(buttonHolder, prefix, item, columnDescriptionMethod)

    undefined

  columnButtonCreator: ->
    for item in @columnList
      button id:"#{@itemPrefix}#{item.Id}", class: "columnNameButton", ->
        "#{item.ColumnName}"