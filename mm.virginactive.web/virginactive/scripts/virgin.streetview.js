var rooms = {
	/* Canary Riverside rooms */
	"reception": {
		name: "Reception",
		club: "Canary Riverside",
		originHeading: 10,
		links: [
		{ 	heading: 290,
			description: "Cafe",
			pano: "cafe"
		},
		{ 	heading: 340,
			description: "Studio",
			pano: "studio"
		},
		{	heading: 10,
			description: "Cardio",
			pano: "cardio"
		}]
	},
  	"cafe": {
		name: "Cafe",
		club: "Canary Riverside",
		originHeading: 0,
		links: [
		{ 	heading: 270,
			description: "Reception",
			pano: "reception"
		}]
  	},
  	"studio": {
		name: "Studio",
		club: "Canary Riverside",
		originHeading: 90,
		links: [
		{	heading: 120,
			description: "Spa",
			pano: "spa"
		},
		{	heading: 90,
			description: "Reception",
			pano: "reception"
		}]
  	},
  	"spa": {
		 name: "Spa",
		club: "Canary Riverside",
		 originHeading: 330,
		 links: [
		 { 	heading: 340,
			description: "Pool",
			pano: "pool"
		 },
		 {	heading: 15,
			description: "Studio",
			pano: "studio"
		 },
		 {	heading: 50,
			description: "Reception",
			pano: "reception"
		 }]
  	},
   	"pool": {
		name: "Pool",
		club: "Canary Riverside",
		originHeading: 225,
		links: [
		{ 	heading: 270,
			description: "Spa",
			pano: "spa"
		}]
  	},
   	"cardio": {
		name: "Cardio",
		club: "Canary Riverside",
		originHeading: 30,
		links: [
		{ 	heading: 20,
			description: "Spin",
			pano: "spin"
		},
		{	heading: 310,
			description: "Free Weights",
			pano: "weights"
		},
		{	heading: 270,
			description: "Reception",
			pano: "reception"
		}]
  	},
   	"spin": {
		name: "Spin",
		club: "Canary Riverside",
		originHeading: 340,
		links: [
		{ 	heading: 15,
			description: "Cardio",
			pano: "cardio"
		}]
  	},
   	"weights": {
		name: "Weights",
		club: "Canary Riverside",
		originHeading: 90,
		links: [
		{ 	heading: 340,
			description: "Cardio",
			pano: "cardio"
		}]
  	},
	/* Northampton rooms */
   	"n-reception": {
		name: "Reception",
		club: "Northampton",
		originHeading: 0,
		links: [
		{ 	heading: 320,
			description: "Pool",
			pano: "n-pool"
		},
		{ 	heading: 359,
			description: "Cardio",
			pano: "n-cardio"
		},
		{ 	heading: 25,
			description: "Cafe",
			pano: "n-cafe"
		}]
  	},
   	"n-cardio": {
		name: "Cardio",
		club: "Northampton",
		originHeading: 0,
		links: [
		{ 	heading: 240,
			description: "Studio 2",
			pano: "n-studio2"
		},
		{ 	heading: 280,
			description: "Studio",
			pano: "n-studio"
		},
		{ 	heading: 320,
			description: "Spin",
			pano: "n-spin"
		},
		{ 	heading: 359,
			description: "Reception",
			pano: "n-reception"
		},
		{ 	heading: 35,
			description: "Cardio",
			pano: "n-cardio2"
		},
		{ 	heading: 80,
			description: "Pool",
			pano: "n-pool"
		}]
  	},
   	"n-cardio2": {
		name: "Cardio",
		club: "Northampton",
		originHeading: 300,
		links: [
		{ 	heading: 280,
			description: "Pool",
			pano: "n-pool"
		},
		{ 	heading: 320,
			description: "Cardio",
			pano: "n-cardio"
		},
		{ 	heading: 5,
			description: "Studio",
			pano: "n-studio"
		},
		{ 	heading: 35,
			description: "Spin",
			pano: "n-spin"
		}]
  	},
   	"n-spin": {
		name: "Cardio",
		club: "Northampton",
		originHeading: 20,
		links: [
		{ 	heading: 180,
			description: "Cardio",
			pano: "n-cardio"
		}]
  	},
   	"n-studio": {
		name: "Studio",
		club: "Northampton",
		originHeading: 120,
		links: [
		{ 	heading: 100,
			description: "Cardio",
			pano: "n-cardio"
		}]
  	},
   	"n-studio2": {
		name: "Studio",
		club: "Northampton",
		originHeading: 0,
		links: [
		{ 	heading: 300,
			description: "Cardio",
			pano: "n-cardio"
		}]
  	},
   	"n-pool": {
		name: "Pool",
		club: "Northampton",
		originHeading: 130,
		links: [
		{ 	heading: 10,
			description: "Cafe",
			pano: "n-cafe"
		},
		{ 	heading: 50,
			description: "Cardio",
			pano: "n-cardio"
		},
		{ 	heading: 90,
			description: "Reception",
			pano: "n-reception"
		},
		{ 	heading: 180,
			description: "Spa",
			pano: "n-spa"
		},
		{ 	heading: 300,
			description: "Kid's Pool",
			pano: "n-pool-kids"
		}]
  	},
   	"n-pool-kids": {
		name: "Kid's Pool",
		club: "Northampton",
		originHeading: 350,
		links: [
		{ 	heading: 250,
			description: "Pool",
			pano: "n-pool"
		}]
  	},
   	"n-spa": {
		name: "Spa",
		club: "Northampton",
		originHeading: 0,
		links: [
		{ 	heading: 280,
			description: "Pool",
			pano: "n-pool"
		}]
  	},
   	"n-cafe": {
		name: "Cafe",
		club: "Northampton",
		originHeading: 300,
		links: [
		{ 	heading: 225,
			description: "Reception",
			pano: "n-reception"
		},
		{ 	heading: 260,
			description: "Cardio",
			pano: "n-cardio"
		},
		{ 	heading: 330,
			description: "Pool",
			pano: "n-pool"
		}]
  	},
	/* West London rooms */
   	"wl-reception": {
		name: "Reception",
		club: "West London",
		originHeading: 0,
		links: [
		{ 	heading: 345,
			description: "Pool",
			pano: "wl-pool"
		},
		{ 	heading: 15,
			description: "Cardio",
			pano: "wl-cardio"
		},
		{ 	heading: 45,
			description: "Cafe",
			pano: "wl-cafe"
		}]
  	},
   	"wl-pool": {
		name: "Pool",
		club: "West London",
		originHeading: 330,
		links: [
		{ 	heading: 235,
			description: "Kid's Pool",
			pano: "wl-pool-kids"
		},
		{ 	heading: 195,
			description: "Spa",
			pano: "wl-spa"
		},
		{ 	heading: 125,
			description: "Reception",
			pano: "wl-reception"
		}]
  	},
   	"wl-pool-kids": {
		name: "Kid's Pool",
		club: "West London",
		originHeading: 40,
		links: [
		{ 	heading: 165,
			description: "Pool",
			pano: "wl-pool"
		},
		{ 	heading: 215,
			description: "Spa",
			pano: "wl-spa"
		}]
  	},
	"wl-spa": {
		name: "Spa",
		club: "West London",
		originHeading: 340,
		links: [
		{ 	heading: 85,
			description: "Pool",
			pano: "wl-pool"
		},
		{ 	heading: 125,
			description: "Kid's Pool",
			pano: "wl-pool-kids"
		}]
  	},
   	"wl-cafe": {
		name: "Cafe",
		club: "West London",
		originHeading: 330,
		links: [
		{ 	heading: 260,
			description: "Tennis Courts",
			pano: "wl-tennis"
		},
		{ 	heading: 55,
			description: "Reception",
			pano: "wl-reception"
		}]
  	},
   	"wl-tennis": {
		name: "Tennis Courts",
		club: "West London",
		originHeading: 120,
		links: [
		{ 	heading: 45,
			description: "Cafe",
			pano: "wl-cafe"
		}]
  	},
   	"wl-cardio": {
		name: "Cardio",
		club: "West London",
		originHeading: 270,
		links: [
		{ 	heading: 100,
			description: "Cardio",
			pano: "wl-cardio2"
		},
		{ 	heading: 145,
			description: "Studio",
			pano: "wl-studio"
		},
		{ 	heading: 190,
			description: "Reception",
			pano: "wl-reception"
		},
		{ 	heading: 250,
			description: "Spin",
			pano: "wl-spin"
		}]
  	},
   	"wl-cardio2": {
		name: "Cardio",
		club: "West London",
		originHeading: 270,
		links: [
		{ 	heading: 130,
			description: "Studio",
			pano: "wl-studio"
		},
		{ 	heading: 170,
			description: "Reception",
			pano: "wl-reception"
		},
		{ 	heading: 210,
			description: "Spin",
			pano: "wl-spin"
		},
		{ 	heading: 255,
			description: "Cardio",
			pano: "wl-cardio"
		}]
  	},
   	"wl-spin": {
		name: "Spin",
		club: "West London",
		originHeading: 340,
		links: [
		{ 	heading: 300,
			description: "Cardio",
			pano: "wl-cardio"
		}]
  	},
   	"wl-studio": {
		name: "Studio",
		club: "West London",
		originHeading: 5,
		links: [
		{ 	heading: 285,
			description: "Cardio",
			pano: "wl-cardio"
		}]
  	}
}

