class window.foundContainer

  constructor: (thePassedInElement, itemList, callMethod) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")

    itemPrefix = "buttonFoundSearchItem_"

    thePassedInElement = addTo thePassedInElement, foundContainer.prototype.searchItemCreator, {itemPrefix:itemPrefix, listData: itemList, callMethod:callMethod}
    thePassedInElement = addTo thePassedInElement, aClearBothDiv, {}
    thePassedInElement =  foundContainer.prototype.setClickEvent(thePassedInElement, itemPrefix, itemList, callMethod)

  setClickEvent: (element, prefix,  itemList, methodToCall) ->
    for item in itemList
      element = setClickForElement(element, prefix, item, methodToCall)
    element

  searchItemCreator: ->
    for item in @listData
      button id:"#{@itemPrefix}#{item.Id}", class: "searchItem floatLeft", ->
        div class: "searchItemLabel", ->
          span "#{item.TableName}"
        div class: "searchItemSchema", ->
          span "#{item.SchemaName}"
        div class: "searchItemDatabase", ->
          span "#{item.DatabaseName}"


