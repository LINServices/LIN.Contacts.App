/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["D:/LIN Services/Components/LIN.Emma.UI/**/*{html,razor,js,cs}", "../**/*{html,razor,js,cs}", "D:/LIN Services/Components/LIN.Contacts.Shared/**/*{html,razor,js,cs}"],
    theme: {
        screens: {
            'sm': '640px',
            'md': '768px',
            'dl': '910px',
            'lg': '1024px',
            'xl': '1280px',
            '2xl': '1536px',
        },
        extend: {
            colors: {
                'current': {
                    '50': '#f3faf5',
                    '100': '#e4f4e9',
                    '200': '#c9e9d3',
                    '300': '#9fd6b2',
                    '400': '#6eba88',
                    '500': '#499e66',
                    '600': '#388151',
                    '700': '#2f6642',
                    '800': '#2d5a3d',
                    '900': '#23442f',
                    '950': '#0f2417'
                }
            }
        },
    },
    plugins: []
}

