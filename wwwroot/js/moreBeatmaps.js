function getMods(mods) {
    const modNames = {
        1: "NF",
        2: "EZ",
        4: "TD",
        8: "HD",
        16: "HR",
        32: "SD",
        64: "DT",
        128: "RX",
        256: "HT",
        512: "NC",
        1024: "FL",
        2048: "Autoplay",
        4096: "SO",
        8192: "AP",
        16384: "PF",
        32768: "Key4",
        65536: "Key5",
        131072: "Key6",
        262144: "Key7",
        524288: "Key8",
        1048576: "FadeIn",
        2097152: "Random",
        4194304: "Cinema",
        8388608: "Target",
        16777216: "Key9",
        33554432: "KeyCoop",
        67108864: "Key1",
        134217728: "Key3",
        268435456: "Key2",
        536870912: "ScoreV2",
        1073741824: "Mirror",
    };

    const modArray = Object.keys(modNames)
        .map(modValue => parseInt(modValue))
        .filter(modValue => mods & modValue)
        .map(modValue => modNames[modValue]);
    
    let modString = modArray.sort((a, b) => modNames[b] - modNames[a]).join("");
    modString = modString.replace("DTNC", "NC").replace("ScoreV2", "V2");

    return modString;
}

async function fetchJsonData() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Wystąpił błąd podczas pobierania danych:', error);
        return null;
    }
}

const toggleButton = document.getElementById('show-hide-topplays');

async function displayJsonDataTopPlays() {
    const scoresData = await fetchJsonData();
    if (scoresData) {
        const playsDiv = document.querySelector(".plays");

        for (let i = 10; i < scoresData.scores.length; i++) {
            const score = scoresData.scores[i];
            const mods = getMods(score.mods);

            const playTime = new Date(score.play_time).toLocaleString();
            const beatmapUrl = `/beatmaps/${score.beatmap.id}`;
            const imageUrl = `https://assets.ppy.sh/beatmaps/${score.beatmap.set_id}/covers/raw.jpg`;

            const html = `
        <a href="${beatmapUrl}">
          <div class="play top-hidden">
            <div class="background" style="--bg:url('${imageUrl}')"></div>
            <div class="map-content">
              <div class="grade rank ${score.grade}">${score.grade}</div>
              <span class="title">${score.beatmap.artist} - ${score.beatmap.title} [${score.beatmap.version}]</span>
              <span class="subtitle">played at ${playTime}</span>
              <span class="mods">${mods}</span>
              <span class="combo">x${score.max_combo}/${score.beatmap.max_combo}</span>
              <span class="acc">${score.acc.toFixed(2)}%</span>
              <span class="pp">${Math.round(score.pp)}pp</span>
            </div>
          </div>
        </a>
      `;

            playsDiv.innerHTML = playsDiv.innerHTML + html;
        }
    }
}

const smt = document.getElementById("show-more-topplays");
smt.addEventListener("click",() => {
    smt.style.display = "none";
    const loading = document.querySelector(".loading");
    loading.style.display = "block";
    displayJsonDataTopPlays().then(() => {
        clearInterval(interval);
        loading.style.display = "none";
        toggleButton.style.display = "block";
    });
});

let contentVisible = true;

toggleButton.addEventListener('click', function() {
    const hiddenElements = document.querySelectorAll('.top-hidden');

    if (!contentVisible) {
        hiddenElements.forEach(element => {
            element.style.display = 'block';
        });
        toggleButton.textContent = 'Hide';
    } else {
        hiddenElements.forEach(element => {
            element.style.display = 'none';
        });
        toggleButton.textContent = 'Show More';
    }

    contentVisible = !contentVisible;
});
