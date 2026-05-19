// script.js
const cities = {
    "Budapest": { lat: 47.4979, lon: 19.0402 },
    "London": { lat: 51.5074, lon: -0.1278 },
    "New York": { lat: 40.7128, lon: -74.0060 },
    "Tokyo": { lat: 35.6762, lon: 139.6503 },
    "Sydney": { lat: -33.8688, lon: 151.2093 },
    "Paris": { lat: 48.8566, lon: 2.3522 },
    "Berlin": { lat: 52.5200, lon: 13.4049 }
};

let currentCity = null;
let weatherData = null;
let filteredData = null;

// API hívás
async function fetchWeather(lat, lon) {
    const loadingEl = document.getElementById('loading');
    if (loadingEl) loadingEl.style.display = 'block';
    
    try {
        const url = `https://api.open-meteo.com/v1/forecast?latitude=${lat}&longitude=${lon}&daily=temperature_2m_max,temperature_2m_min,precipitation_probability_max&timezone=Europe/Budapest`;
        
        const response = await fetch(url);
        if (!response.ok) throw new Error('Hálózati hiba');
        
        const data = await response.json();
        weatherData = data.daily;
        filteredData = [...weatherData.time]; // indexek
        
        if (loadingEl) loadingEl.style.display = 'none';
        return data;
    } catch (error) {
        console.error(error);
        alert('Hiba történt az adatok betöltése közben: ' + error.message);
        if (loadingEl) loadingEl.style.display = 'none';
        return null;
    }
}

// Város lista feltöltése
function populateCities() {
    const select = document.getElementById('city-select');
    select.innerHTML = '<option value="">Válassz várost...</option>';
    
    Object.keys(cities).forEach(city => {
        const option = document.createElement('option');
        option.value = city;
        option.textContent = city;
        select.appendChild(option);
    });
}

// Város kiválasztása
async function selectCity() {
    const select = document.getElementById('city-select');
    const cityName = select.value;
    
    if (!cityName) return;
    
    currentCity = cityName;
    const coords = cities[cityName];
    
    document.getElementById('selected-city-info').innerHTML = `
        <h3>Kiválasztott város: <strong>${cityName}</strong></h3>
        <p>Koordináták: ${coords.lat}, ${coords.lon}</p>
        <button onclick="navigateTo('forecast')" class="btn-primary">Előrejelzés betöltése</button>
    `;
    
    // Automatikusan betöltjük az előrejelzést is
    await fetchWeather(coords.lat, coords.lon);
}

// Kártya generálás
function createForecastCard(dayIndex) {
    const date = new Date(weatherData.time[dayIndex]);
    const dayName = date.toLocaleDateString('hu-HU', { weekday: 'long' });
    const formattedDate = date.toLocaleDateString('hu-HU');
    
    const maxTemp = Math.round(weatherData.temperature_2m_max[dayIndex]);
    const minTemp = Math.round(weatherData.temperature_2m_min[dayIndex]);
    const precip = weatherData.precipitation_probability_max[dayIndex];
    
    let tempClass = 'moderate';
    let description = 'mérsékelt';
    
    if (maxTemp > 25) {
        tempClass = 'hot';
        description = 'meleg';
    } else if (maxTemp < 10) {
        tempClass = 'cold';
        description = 'hideg';
    }
    
    return `
        <div class="card">
            <h3>${dayName}</h3>
            <p>${formattedDate}</p>
            <div class="temp ${tempClass}">${maxTemp}° / ${minTemp}°</div>
            <p>🌧️ Csapadék: ${precip}%</p>
            <p><strong>${description}</strong></p>
        </div>
    `;
}

// Előrejelzés megjelenítése
function showForecast() {
    if (!weatherData) return;
    
    const grid = document.getElementById('forecast-grid');
    grid.innerHTML = '';
    
    for (let i = 0; i < weatherData.time.length; i++) {
        grid.innerHTML += createForecastCard(i);
    }
    
    document.getElementById('forecast-title').textContent = 
        `${currentCity} - 7 napos előrejelzés`;
}

// Szűrők
function filterRainy() {
    if (!weatherData) return;
    const grid = document.getElementById('filter-grid');
    grid.innerHTML = '';
    
    for (let i = 0; i < weatherData.time.length; i++) {
        if (weatherData.precipitation_probability_max[i] > 40) {
            grid.innerHTML += createForecastCard(i);
        }
    }
}

function filterHot() {
    if (!weatherData) return;
    const grid = document.getElementById('filter-grid');
    grid.innerHTML = '';
    
    for (let i = 0; i < weatherData.time.length; i++) {
        if (weatherData.temperature_2m_max[i] > 25) {
            grid.innerHTML += createForecastCard(i);
        }
    }
}

function resetFilter() {
    const grid = document.getElementById('filter-grid');
    grid.innerHTML = '';
    for (let i = 0; i < weatherData.time.length; i++) {
        grid.innerHTML += createForecastCard(i);
    }
}

// Heti bontás / Összefoglaló
function showWeekly() {
    if (!weatherData) return;
    
    const summary = document.getElementById('weekly-summary');
    const grid = document.getElementById('weekly-grid');
    
    let totalMax = 0;
    let hottestDay = 0;
    let rainiestDay = 0;
    let maxRain = 0;
    
    weatherData.temperature_2m_max.forEach((temp, i) => {
        totalMax += temp;
        if (temp > weatherData.temperature_2m_max[hottestDay]) hottestDay = i;
        if (weatherData.precipitation_probability_max[i] > maxRain) {
            maxRain = weatherData.precipitation_probability_max[i];
            rainiestDay = i;
        }
    });
    
    const avgTemp = (totalMax / weatherData.time.length).toFixed(1);
    
    summary.innerHTML = `
        <div class="card" style="grid-column: 1 / -1; text-align: center;">
            <h3>Heti összefoglaló - ${currentCity}</h3>
            <p><strong>Átlagos max hőmérséklet:</strong> ${avgTemp}°C</p>
            <p><strong>Legmelegebb nap:</strong> ${new Date(weatherData.time[hottestDay]).toLocaleDateString('hu-HU')}</p>
            <p><strong>Legcsapadékosabb nap:</strong> ${new Date(weatherData.time[rainiestDay]).toLocaleDateString('hu-HU')} (${maxRain}%)</p>
        </div>
    `;
    
    grid.innerHTML = '';
    for (let i = 0; i < weatherData.time.length; i++) {
        grid.innerHTML += createForecastCard(i);
    }
}

// Statisztikák
function showStats() {
    if (!weatherData) return;
    
    const content = document.getElementById('stats-content');
    content.innerHTML = '';
    
    let sumTemp = 0;
    let maxTemp = -Infinity;
    let maxTempDay = 0;
    let maxPrecip = -Infinity;
    let maxPrecipDay = 0;
    
    weatherData.temperature_2m_max.forEach((temp, i) => {
        sumTemp += temp;
        if (temp > maxTemp) {
            maxTemp = temp;
            maxTempDay = i;
        }
        if (weatherData.precipitation_probability_max[i] > maxPrecip) {
            maxPrecip = weatherData.precipitation_probability_max[i];
            maxPrecipDay = i;
        }
    });
    
    const avg = (sumTemp / weatherData.time.length).toFixed(1);
    
    content.innerHTML = `
        <div class="card">
            <h3>Átlaghőmérséklet</h3>
            <p style="font-size: 2.5rem; font-weight: bold;">${avg}°C</p>
        </div>
        <div class="card">
            <h3>Legmelegebb nap</h3>
            <p>${new Date(weatherData.time[maxTempDay]).toLocaleDateString('hu-HU')}</p>
            <p style="font-size: 2rem;">${maxTemp}°C</p>
        </div>
        <div class="card">
            <h3>Legtöbb csapadék</h3>
            <p>${new Date(weatherData.time[maxPrecipDay]).toLocaleDateString('hu-HU')}</p>
            <p style="font-size: 2rem;">${maxPrecip}%</p>
        </div>
    `;
}

// Navigáció
function navigateTo(page) {
    document.querySelectorAll('.page').forEach(p => p.classList.add('hidden'));
    
    if (page === 'home') {
        document.getElementById('home-page').classList.remove('hidden');
    } else if (page === 'cities') {
        document.getElementById('cities-page').classList.remove('hidden');
    } else if (page === 'forecast') {
        if (!currentCity) {
            alert('Előbb válassz várost!');
            navigateTo('cities');
            return;
        }
        document.getElementById('forecast-page').classList.remove('hidden');
        showForecast();
    } else if (page === 'filter') {
        if (!currentCity) {
            alert('Előbb válassz várost!');
            navigateTo('cities');
            return;
        }
        document.getElementById('filter-page').classList.remove('hidden');
        resetFilter();
    } else if (page === 'weekly') {
        if (!currentCity) {
            alert('Előbb válassz várost!');
            navigateTo('cities');
            return;
        }
        document.getElementById('weekly-page').classList.remove('hidden');
        showWeekly();
    } else if (page === 'stats') {
        if (!currentCity) {
            alert('Előbb válassz várost!');
            navigateTo('cities');
            return;
        }
        document.getElementById('stats-page').classList.remove('hidden');
        showStats();
    }
}

// Inicializálás
document.addEventListener('DOMContentLoaded', () => {
    populateCities();
    navigateTo('home');
    
    // Demo adatok betöltése Budapesttel
    setTimeout(() => {
        currentCity = "Budapest";
        fetchWeather(47.4979, 19.0402).then(() => {
            // Ha akarjuk, előre is betölthetjük valamit
        });
    }, 800);
});