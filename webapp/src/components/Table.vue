<template>
  <div>
    <!-- Filter options -->
    <b-form-group label-cols="4" label-cols-lg="2" label-size="lg" label="Filteroptionen">
      <b-form-select label-cols-sm="4" label-cols-lg="3" v-model="filterBy" :options="filterOptions"></b-form-select>
    </b-form-group>
    <!-- Table with loaded items -->
    <b-table  
    :items="items" 
    :fields="fields" 
    @row-clicked="onRowClicked"
    responsive="sm"
    Fixed
    no-border-collapse
    Outlined
    :tbody-tr-class="rowClass"
    :filter="filterBy"
    :filter-function="filterTable">
      <template v-slot:cell(verfügbar)="{ item }">
        <template v-if="item.verfügbar && item.aktiv"><td class="available"></td></template>
        <template v-else><td class="not_available"></td></template>
      </template>
      <template v-slot:cell(datumAusleihe)="{ item }">
        <template v-if="item.verfügbar || !item.aktiv">-</template>
        <template v-else>{{item.datumAusleihe.substring(0,10)}}</template>
      </template>
      <template v-slot:cell(ausgeliehenAn)="{ item }">
        <template v-if="item.verfügbar || !item.aktiv">-</template>
        <template v-else>{{item.ausgeliehenAn}}</template>
      </template>
    </b-table>
    <!-- Button for add device -->
    <div class="button-div">
      <b-button class="button-add" @click="showAddOrEditModal(true)">
        Neues Gerät hinzufügen
      </b-button>
    </div>
    <!-- Modals -->
    <!-- Modal for asking for clicked device -->
    <b-modal ref="askModal" hide-footer title="Was willst du mit dem Gerät machen?">
      <b-button class="mt-3" variant="outline-danger" block @click="showAddOrEditModal(false)">Bearbeiten</b-button>
      <b-button class="mt-3" variant="outline-danger" block @click="showDeleteModal">Löschen</b-button>
    </b-modal>
    <!-- Modal to add or edit device -->
    <b-modal ref="addOrEditModal" hide-footer :title="helper ? 'Gerät hinzufügen' : 'Gerät bearbeiten'">
      <div class="d-block text-center">
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Aktiv">
          <b-form-checkbox type="checkbox" @change="setDefaultDate($event)" v-model="gerät.aktiv"></b-form-checkbox>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Modell">
          <b-form-input v-model="gerät.modell"></b-form-input>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Baujahr">
          <b-form-input @change="setMaxInput" id="baujahrInput" max="2020" type="number" v-model="gerät.baujahr"></b-form-input>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Seriennummer">
          <b-form-input v-model="gerät.seriennummer"></b-form-input>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Historie">
          <b-form-input v-model="gerät.historie"></b-form-input>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Bemerkung">
          <b-form-input v-model="gerät.bemerkung"></b-form-input>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Verfügbar">
          <b-form-checkbox @change="setDefaultDate($event)" type="checkbox" v-model="gerät.verfügbar"></b-form-checkbox>
        </b-form-group>
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Datum der Ausleihe">
          <b-form-input v-if="!this.gerät.verfügbar" type="date" v-model="gerät.datumAusleihe"></b-form-input>
        </b-form-group> 
        <b-form-group label-cols-sm="4" label-cols-lg="3" label="Ausgeliehen an">
          <b-form-input v-if="!this.gerät.verfügbar" v-model="gerät.ausgeliehenAn"></b-form-input>
        </b-form-group> 
      </div>
      <b-button class="mt-3" variant="outline-danger" block @click="confirmAddOrEdit">Bestätigen</b-button>
      <b-button class="mt-3" variant="outline-danger" block @click="hideAddOrEditModal">Fenster schließen</b-button>
    </b-modal>
    <!-- Modal to delete clicked device -->
    <b-modal ref="deleteModal" hide-footer title="Willst du das Gerät wirklich löschen?">
      <b-button class="mt-3" variant="outline-danger" block @click="confirmDeletion">Ja</b-button>
      <b-button class="mt-3" variant="outline-danger" block @click="hideDeleteModal">Nein</b-button>
    </b-modal>
  </div>
</template>

<script>
  import Vue from 'vue';
  import moment from 'moment';
  export default {
    name: 'Table',
    data(){
      return {
        gerät: {
          geräteID: '',
          modell: '',
          baujahr: '',
          seriennummer: '',
          datumAusleihe: '',
          ausgeliehenAn: '',
          verfügbar: '',
          historie: '',
          bemerkung: '',
          aktiv: ''
        },
        clicked: {},
        sortDesc: true,
        helper: false, // is true when device shell be added and false when it shell be edited
        filterOptions: ['Alle', 'Verfügbare Geräte', 'Ausgeliehene Geräte', 'Aktive Geräte', 'Deaktivierte Geräte'],
        filterBy: 'Alle',
        currentYear: parseInt(new Date().getFullYear())
      }
    },
    props: ['items', 'fields'],
    methods: {

      /* Adding and Editing */
      // Function for show modal for add or edit device
      showAddOrEditModal(para){
        this.$refs['askModal'].hide()
        this.helper = para;
        // Add device
        if(this.helper){
          this.gerät.modell = ''
          this.gerät.baujahr = 2020
          this.gerät.seriennummer = ''
          this.gerät.datumAusleihe =  moment('2020-01-01').format('YYYY-MM-DD')
          this.gerät.ausgeliehenAn = ''
          this.gerät.verfügbar = true
          this.gerät.historie = ''
          this.gerät.bemerkung = ''
          this.gerät.aktiv = true
        } 
        // Edit device
        else {
          this.gerät.geräteID = this.clicked.geräteID
          this.gerät.modell = this.clicked.modell
          this.gerät.baujahr = parseInt(this.clicked.baujahr)
          this.gerät.seriennummer = this.clicked.seriennummer
          this.gerät.datumAusleihe = moment(this.clicked.datumAusleihe.toString()).format('YYYY-MM-DD')
          this.gerät.ausgeliehenAn = this.clicked.ausgeliehenAn
          this.gerät.verfügbar = Boolean(this.clicked.verfügbar)
          this.gerät.historie = this.clicked.historie
          this.gerät.bemerkung = this.clicked.bemerkung
          this.gerät.aktiv = this.clicked.aktiv
        }
        this.$refs['addOrEditModal'].show()
      },
      // Function to confirm for add or edit device
      confirmAddOrEdit(){
        var request = {
          Modell: this.gerät.modell,
          Baujahr: parseInt(this.gerät.baujahr),
          Seriennummer: this.gerät.seriennummer,
          DatumAusleihe: this.gerät.datumAusleihe,
          AusgeliehenAn: this.gerät.ausgeliehenAn,
          Verfügbar: this.gerät.verfügbar,
          Historie: this.gerät.historie,
          Bemerkung: this.gerät.bemerkung,
          Aktiv: this.gerät.aktiv
        }
        // Add device
        if (this.helper){
          Vue.axios.post('/api/commands/', request)
          .then(() => {
            this.$emit('reloadData')
          }); 
        } 
        // Edit Device
        else {
          var id = this.clicked.geräteID
          Vue.axios.put('/api/commands/' + id, request).then(() => {
            this.$emit('reloadData')
          }); 
        }
        this.hideAddOrEditModal()
      },
      // Function to hide modal for add or edit device
      hideAddOrEditModal(){
        this.$refs['addOrEditModal'].hide()
      },

      /* Deletion */
      // Function to show the delete modal
      showDeleteModal(){
        this.$refs['askModal'].hide()
        this.$refs['deleteModal'].show()
      },
      // Function to confirm for delete device
      confirmDeletion(){
        this.$refs['deleteModal'].hide()
        var id = this.clicked.geräteID
        Vue.axios.delete('/api/commands/' + id).then(() => {
          this.$emit('reloadData')
        }); 
        this.hideDeleteModal()
      },
      // Function to hide modal for delete device
      hideDeleteModal(){
        this.$refs['deleteModal'].hide()
      },

      /* Helper functions */
      // Function for open modal when device is clicked
      onRowClicked(item){
        this.clicked = item
        this.$refs['askModal'].show()
      },
      // Function to set default values for datumAusleihe und ausgeliehenAn
      setDefaultDate(e){
        if(!e){
          this.gerät.datumAusleihe = moment('2020-01-01').format('YYYY-MM-DD')
          this.gerät.ausgeliehenAn = ''
          this.gerät.verfügbar = false
        }
      },
      // Function to set devices inactiv
      rowClass(item){
        if(!item.aktiv){
          return 'table-secondary'
        }
      },
      // Function to filter the table 
      filterTable(row, filter) {
        switch(filter){
          case 'Verfügbare Geräte':
            return ((row.verfügbar && row.aktiv) ? true : false)
          case 'Ausgeliehene Geräte':
            return ((!row.verfügbar && row.aktiv) ? true : false)            
          case 'Aktive Geräte':
            return ( row.aktiv ? true : false)            
          case 'Deaktivierte Geräte':
            return (!row.aktiv ? true : false)          
          default:
            return true
        }
      },
      // Set the current year as an max for the input
      setMaxInput(){
        document.getElementById("baujahrInput").setAttribute("max", this.currentYear);
      },
   
   }
 }
</script>


<style scoped>
.available { 
  background-color:#86b37a;
}
.not_available { 
  background-color:#f17269;
}
.button-div {
  position: fixed;  
  bottom: 3%;
  left: 50%;
  transform: translate(-50%, 0);
}
.button-add {
  background-color:#86b37a;
 }
</style>
