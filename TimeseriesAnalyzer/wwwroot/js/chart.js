window.drawModernBitcoinChart = async (bitcoinPrices) => {
    const canvas = document.getElementById("bitcoinChart");
    if (!canvas) {
        console.error("❌ Chart canvas not found!");
        return;
    }

    const ctx = canvas.getContext("2d");
    if (!ctx) {
        console.error("❌ Cannot get canvas context!");
        return;
    }

    new Chart(ctx, {
        type: "line",
        data: {
            labels: bitcoinPrices,
            datasets: [{
                label: "Bitcoin Price (USD)",
                data: bitcoinPrices,
                borderColor: "#FFA500",
                backgroundColor: "rgba(255, 165, 0, 0.2)",
                borderWidth: 2,
                fill: true,
                tension: 0.4, // Smooth lines
                pointRadius: 0, // Hide dots
            }]
        },
        options: { responsive: true }
    });
};


//options: {
  //  responsive: false, // Disable auto-resizing
    //    maintainAspectRatio: false, // Allow custom height
      //  plugins: { legend: { display: false } },
    //scales: { x: { display: false }, y: { display: false } }
//}
