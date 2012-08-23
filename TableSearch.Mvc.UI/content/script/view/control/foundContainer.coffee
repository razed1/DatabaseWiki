class window.foundContainer

  constructor: (thePassedInElement, callMethod) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")

    idBag =
      buttonFoundPrefix: "buttonFoundSearchItem_"
      searchItemDatabaseName: "searchItemDatabase"
      searchItemLabelName: "searchItemLabel",
      searchItemSchemaName: "searchItemSchema",


    proto = foundContainer.prototype
    proto.addTableButtonsMethodTo thePassedInElement, proto.createSetTableButtonsMethod(idBag), callMethod, idBag

  setClickEvent: (element, prefix,  itemList, methodToCall) ->
    for item in itemList
      element = setClickForElement(element, prefix, item, methodToCall)
    element

  addTableButtonsMethodTo : (element, createButtons, callMethod, idNames) ->
    element[0].updateResults = (result) ->
      proto = foundContainer.prototype

      createButtons element, result.Value
      addTo element, aClearBothDiv, {}
      proto.setClickEvent element, idNames.buttonFoundPrefix, result.Value, callMethod
      element.show()

      undefined

  createSetTableButtonsMethod: (idNames) ->
    setTableButtons = (container, itemList) ->
      proto = foundContainer.prototype
      container.children().remove()
      addTo container, proto.searchItemCreator, {idNames:idNames, itemPrefix : idNames.buttonFoundPrefix, listData: itemList}

      undefined

  searchItemCreator: ->
    for item in @listData
      button id:"#{@itemPrefix}#{item.Id}", class: "searchItem floatLeft", ->
        div id: @idNames.searchItemLabelName, class: @idNames.searchItemLabelName, ->
          span "#{item.TableName}"
        div id: @idNames.searchItemSchemaName, class: @idNames.searchItemSchemaName, ->
          span "#{item.SchemaName}"
        div id:@idNames.searchItemDatabaseName, class: @idNames.searchItemDatabaseName, ->
          span "#{item.DatabaseName}"


