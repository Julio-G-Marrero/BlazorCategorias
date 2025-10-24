let toastifyReady = false;

function ensureToastifyAssets() {
    return new Promise((resolve) => {
        if (toastifyReady || window.Toastify) {
            toastifyReady = true;
            return resolve();
        }

        // Cargar CSS
        if (!document.querySelector('link[href*="toastify.min.css"]')) {
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.href = 'https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css';
            document.head.appendChild(link);
        }

        // Cargar JS
        if (!document.querySelector('script[src*="toastify-js"]')) {
            const script = document.createElement('script');
            script.src = 'https://cdn.jsdelivr.net/npm/toastify-js';
            script.onload = () => {
                toastifyReady = true;
                resolve();
            };
            document.body.appendChild(script);
        } else {
            const wait = () => {
                if (window.Toastify) {
                    toastifyReady = true;
                    resolve();
                } else {
                    setTimeout(wait, 50);
                }
            };
            wait();
        }
    });
}

function showToast(message, type) {
    ensureToastifyAssets().then(() => {
        const colors = {
            info: "#3498db",
            success: "#2ecc71",
            error: "#e74c3c",
            warning: "#f1c40f"
        };

        const style = {
            background: colors[type] || "#333",
            color: type === "warning" ? "#000" : "#fff"
        };

        window.Toastify({
            text: message,
            duration: 3000,
            style
        }).showToast();
    });
}


export function showSuccess(message) {
    showToast(message, "success");
}

export function showInfo(message) {
    showToast(message, "info");
}
export function showError(message) {
    showToast(message, "error");
}

export function showWarning(message) {
    showToast(message, "warning");
}

