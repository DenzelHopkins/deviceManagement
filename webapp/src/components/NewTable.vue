<template>
    <div>
        <v-app>
            <v-data-table
            v-model="selected"
            :headers="headers"
            :items="items"
            show-select
            single-select
            item-key="geräteID"
            >
            </v-data-table>  
        </v-app>
    </div>
</template>

<script>
import Vuetify from 'vuetify'
export default {
    name: 'NewTable',
    vuetify: new Vuetify(),
    props: ['items', 'fields'],
    data(){
        return {
            selected: [],
            headers: [
                {
                    text: 'ID',
                    align: 'start',
                    value: 'geräteID'
                },
                {
                    text: 'Modell',
                    value: 'modell'
                },
                {
                    text: 'Baujahr',
                    value: 'baujahr'
                },
                {
                    text: 'Seriennummer',
                    value: 'seriennummer'
                },
                {
                    text: 'Historie',
                    value: 'historie'
                },
                {
                    text: 'Bemerkung',
                    value: 'bemerkung'
                },
                {
                    text: 'Datum der Ausleihe',
                    value: 'datumAusleihe'
                },
                {
                    text: 'Ausgeliehen an ',
                    value: 'ausgeliehenAn'
                },
                {
                    text: 'Verfügbar',
                    value: 'verfügbar'
                }
            ],
        }
    }
}
</script>