window.aClearBothDiv = ->
  div class:"clearBoth"


window.addTo = (element, creator, extraParameters) ->
  element.append(CoffeeKup.render(creator, extraParameters))
  element


window.setClickForElement = (element, prefix, item, methodToCall) ->
    element.find("##{prefix}#{item.Id}").click -> methodToCall(item.Id)
    element