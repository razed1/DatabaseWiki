class window.columnDescriptionContainer

  constructor: (thePassedInElement) ->
    if !thePassedInElement?
      throw new Error("dragAndDropList: No element was passed in.")

    proto = columnDescriptionContainer.prototype
    thePassedInElement = addTo thePassedInElement, proto.columnDescriptionContainerCreator, {}
    proto.addUpdateDescriptionMethodTo thePassedInElement
    proto.addHideMethodTo thePassedInElement

  addHideMethodTo: (element) ->
    element[0].hideFieldset = (result) ->
      fieldSet = jQuery(element).find(".fieldsetColumnDescriptionText")
      fieldSet.hide()

  addUpdateDescriptionMethodTo : (element) ->
    element[0].updateDescription = (result) ->
      fieldSet = jQuery(element).find(".fieldsetColumnDescriptionText")
      fieldSet.find(".divDescriptionText").text(result.Description)
      fieldSet.find("#legend").text(result.ColumnName)
      fieldSet.show()
      undefined

  columnDescriptionContainerCreator: ->
    fieldset class: "fieldsetColumnDescriptionText", style: "display:none;", ->
      legend id: "legend", ->
        ""
      div class: "divDescriptionText", ->
        ""
