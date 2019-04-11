console.clear();
app = angular.module('Test', []);
app.controller('ProductController', function($scope,$http) {
  $scope.foods = [
    {
      "selectproduct": "",
      "Quantity1": "",
      "Quantity2": ""
    }
  ];

  $scope.cloneItem = function () {
    var itemToClone = { "selectproduct": "", "Quantity1": "", "Quantity2": "" };
    $scope.foods.push(itemToClone);
  }

  $scope.removeItem = function (itemIndex) {
    $scope.foods.splice(itemIndex, 1);
  }
          
  $scope.updateBarcode = function(){
  	var barcode = new bytescoutbarcode128();
    var space= "  ";
		var value = document.getElementById("barcodeValue").value;
		var value1 = document.getElementById("barcodeValue1").value;
		var value2 = document.getElementById("barcodeValue2").value;

		barcode.valueSet(value + space + value1 + space + value2);
		barcode.setMargins(5, 5, 5, 5);
		barcode.setBarWidth(2);

		var width = barcode.getMinWidth();

		barcode.setSize(width, 100);

		var barcodeImage = document.getElementById('barcodeImage');
		barcodeImage.src = barcode.exportToBase64(width, 100, 0);
  }
  
  //$scope.updateBarcode();
});
app.directive('barcode', function(){
  return{
    restrict: 'AE',
    template: '<img id="barcodeImage" style="border: solid 1px black;" src="{{src}}"/>',
    scope: {
      food: '='
    },
    link: function($scope){
      $scope.$watch('food', function(food){
        console.log($scope.food);
        var barcode = new bytescoutbarcode128();
        var space= "  ";

    		barcode.valueSet([$scope.food.selectproduct, $scope.food.Quantity1, $scope.food.Quantity2].join(space));
    		barcode.setMargins(5, 5, 5, 5);
    		barcode.setBarWidth(2);
    
    		var width = barcode.getMinWidth();
    
    		barcode.setSize(width, 100);

    		$scope.src = barcode.exportToBase64(width, 100, 0);
      }, true);
    }
  }
});
