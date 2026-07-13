let sourceArr = new Array(5).fill(0).map((_, index) => index + 1); // [1, 2, 3, 4, 5]

console.log("Before splice:", sourceArr);

sourceArr.splice(2, 1); // Remove the element at index 2 (which is the number 3)

console.log("After splice:", sourceArr); // Output: [1, 2, 4, 5]
