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
                    '50': '#f3faf9',
                    '100': '#d6f1ea',
                    '200': '#ace3d6',
                    '300': '#7bcdbc',
                    '400': '#4fb2a1',
                    '500': '#369688',
                    '600': '#29786d',
                    '700': '#276860',
                    '800': '#214e49',
                    '900': '#1f423d',
                    '950': '#0d2625'
                }
            }
        },
    },
    plugins: []
}

