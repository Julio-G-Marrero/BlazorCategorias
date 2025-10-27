let animeReadyPromise = null;
function ensureAnimeJs() {
    if (animeReadyPromise) return animeReadyPromise;
    animeReadyPromise = new Promise((resolve, reject) => {
        if (window.anime) return resolve();
        const s = document.createElement('script');
        s.src = 'https://cdn.jsdelivr.net/npm/animejs@3.2.1/lib/anime.min.js';
        s.onload = () => resolve();
        s.onerror = (e) => reject(e);
        document.body.appendChild(s);
    });
    return animeReadyPromise;
}

function merge(base, extra) {
    return Object.assign({}, base || {}, extra || {});
}

const intensityMap = {
    soft: { bgDur: 180, dlgDur: 220, scaleFrom: 0.96, yFrom: -12, easing: 'easeOutQuad' },
    medium: { bgDur: 220, dlgDur: 280, scaleFrom: 0.94, yFrom: -18, easing: 'easeOutCubic' },
    strong: { bgDur: 260, dlgDur: 360, scaleFrom: 0.9, yFrom: -24, easing: 'easeOutBack' }
};

export async function animateModalOpen(selector, opts = {}) {
    await ensureAnimeJs();
    const overlay = document.querySelector(selector);
    if (!overlay) return;

    const preset = intensityMap[opts.intensity || 'medium'];

    // Gradual blur
    window.anime(merge({
        targets: overlay,
        opacity: [0, 1],
        duration: preset.bgDur + 100,
        easing: 'linear',
        update: (anim) => {
            overlay.style.backdropFilter = `blur(${(anim.progress / 100) * (opts.blur || 3)}px)`;
        }
    }, opts.overlay));

    const dialog = overlay.querySelector('.modal-dialog');
    if (dialog) {
        if (opts.dropShadow) {
            dialog.style.filter = `drop-shadow(0 10px 30px rgba(0,0,0,0.3))`;
        }
        window.anime(merge({
            targets: dialog,
            translateY: [preset.yFrom * 1.6, 0],
            scale: [0.88, 1.06, 0.98, 1],
            opacity: [0, 1],
            duration: preset.dlgDur + 220,
            easing: 'easeOutElastic(1, .7)'
        }, opts.dialog));
    }
}

export async function pulseRow(selector, opts = {}) {
    await ensureAnimeJs();
    const row = document.querySelector(selector);
    if (!row) return;

    // “Glow” + fondo
    const base = {
        targets: row,
        backgroundColor: ['#fff2b3', '#ffffff'],
        duration: opts.duration || 900,
        easing: opts.easing || 'easeInOutQuad'
    };

    if (opts.glow) {
        row.style.boxShadow = '0 0 0 rgba(255,193,7,0.0)';
        window.anime({
            targets: row,
            boxShadow: ['0 0 0 rgba(255,193,7,0.0)', '0 0 16px rgba(255,193,7,0.6)', '0 0 0 rgba(255,193,7,0.0)'],
            duration: base.duration,
            easing: base.easing
        });
    }

    window.anime(base);
}

export async function fadeOutRow(selector, opts = {}) {
    await ensureAnimeJs();
    const row = document.querySelector(selector);
    if (!row) return;

    row.classList.add('row-deactivated');

    await window.anime({
        targets: row,
        backgroundColor: ['#fff9c4', '#ffecec'], 
        color: ['#212529', '#6c757d'],           
        opacity: [1, 0.65],
        duration: opts.duration || 450,
        easing: opts.easing || 'easeInOutQuad'
    }).finished;

    if (opts.glow) {
        row.style.boxShadow = '0 0 0 rgba(220,53,69,0)';
        await window.anime({
            targets: row,
            boxShadow: [
                '0 0 0 rgba(220,53,69,0)',
                '0 0 16px rgba(220,53,69,0.55)',
                '0 0 0 rgba(220,53,69,0)'
            ],
            duration: 600,
            easing: 'easeInOutQuad'
        }).finished;
    }
}

export async function restoreRow(selector, opts = {}) {
    await ensureAnimeJs();
    const row = document.querySelector(selector);
    if (!row) return;

    await window.anime({
        targets: row,
        backgroundColor: ['#ffecec', '#ffffff'],
        color: ['#6c757d', '#212529'],
        opacity: [0.65, 1],
        duration: opts.duration || 300,
        easing: opts.easing || 'easeInOutQuad'
    }).finished;

    row.classList.remove('row-deactivated');
}

export async function staggerRows(tbodySelector, opts = {}) {
    await ensureAnimeJs();
    const rows = document.querySelectorAll(`${tbodySelector} > tr`);
    if (!rows.length) return;
    window.anime({
        targets: rows,
        opacity: [0, 1],
        translateY: [8, 0],
        duration: opts.duration || 280,
        delay: window.anime.stagger(opts.delay || 30),
        easing: opts.easing || 'easeOutQuad'
    });
}
export async function animateModalClose(selector, opts = {}) {
    await ensureAnimeJs();
    const overlay = document.querySelector(selector);
    if (!overlay) return;

    const dialog = overlay.querySelector('.modal-dialog');
    const duration = opts.duration || 280;
    if (dialog) {
        await window.anime({
            targets: dialog,
            translateY: [0, -12],
            scale: [1, 0.95],
            opacity: [1, 0],
            duration,
            easing: opts.easing || 'easeInCubic'
        }).finished;
    }

    await window.anime({
        targets: overlay,
        opacity: [1, 0],
        duration: duration - 50,
        easing: opts.easing || 'linear'
    }).finished;
}
