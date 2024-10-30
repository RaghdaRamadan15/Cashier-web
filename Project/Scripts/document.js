let iskeydown = false
let isMouseOver = false;
window.onload = setv;

document.addEventListener('keydown', function (event) {
	iskeydown = true
	//console.log(event.key)
})

document.addEventListener('keyup', function (event) {
	iskeydown = false
	//console.log(event.key)
})

document.addEventListener('mouseover', function () {
	isMouseOver = true;
	//console.log('kkk')

})



document.addEventListener('mouseout', function () {
	isMouseOver = false;
	//console.log('ll')

})

function setv() {
	let time = 0;
	let intervalId = setInterval(() => {
		time += 1;
		if (time >= 120) {
			if (iskeydown == true || isMouseOver == true)
			{
				time = 0;
			}
			else
			{
				let exist = confirm('هل تريد استمرار في استخدام موقع؟');
				if (exist) {
					time = 0;

				}
				else
				{
					clearInterval(intervalId);
				}
		    }
		
		
		}

	}


	, 1000)
}




