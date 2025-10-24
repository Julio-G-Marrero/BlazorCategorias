window.toastHelper = {
    show: function (message, type = "info", ms = 3000) {
        const colors = {
            info: "linear-gradient(to right, #00b09b, #96c93d)",
            success: "linear-gradient(to right, #4caf50, #81c784)",
            error: "linear-gradient(to right, #e53935, #e35d5b)",
            warn: "linear-gradient(to right, #f6d365, #fda085)"
        };

        Toastify({
            text: message,
            duration: ms,
            gravity: "top",
            position: "right",
            style: { background: colors[type] || colors.info }
        }).showToast();
    }
};
