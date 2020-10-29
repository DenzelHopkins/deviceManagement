<template>
    <div id="app">
        <Title msg="Geräteverwaltung"/>
         <Table v-on:reloadData="load" v-bind:items="items" v-bind:fields="fields"></Table>
        <!--<NewTable v-on:reloadData="load" v-bind:items="items" v-bind:fields="fields"></NewTable>-->
    </div>
</template>

<script>
    import Title from './components/Title.vue';
    import Table from './components/Table.vue';
    import NewTable from './components/NewTable.vue';
    import Vue from 'vue';
    export default {
        name: 'app',
        components: {
            Title,
            Table, 
            NewTable
        },
        data() {
            return {
            fields: [
                {key: 'geräteID', label:'GeräteID', sortable: true},
                {key: 'modell', label:'Modell', sortable: true},
                {key: 'baujahr', label:'Baujahr', sortable: true},
                {key: 'seriennummer', label:'Seriennummer', sortable: true},
                {key: 'historie', label:'Historie'},
                {key: 'bemerkung', label:'Bemerkung'},
                {key: 'datumAusleihe', label:'Datum der Ausleihe', sortable: true},
                {key: 'ausgeliehenAn', label:'Ausgeliehen an', sortable: true},
                {key: 'verfügbar', label:'Verfügbar', sortable: true}
            ],
            items: []
            }
        },
        methods: {
            load: function() {
                Vue.axios.get('https://' + process.env.VUE_APP_SERVER_NAME + ':' + process.env.VUE_APP_SERVER_PORT + '/api/commands').then((response) => {
                    console.log(response.data)
                    this.items = response.data;
                });
            }
        },
        mounted: function(){
                this.load();
            },
    };
</script>

<style>
</style>

