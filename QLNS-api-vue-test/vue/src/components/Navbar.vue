!<template>
    <b-navbar type="dark" variant="info" id="nav-menu" v-show="this.$store.state.isLoggedIn">
      <b-navbar-nav>
        <b-nav-item>
          <router-link  v-show="!this.$store.state.isEmployee" to="/">Home</router-link>
        </b-nav-item>
         <b-nav-item>
          <router-link v-show="this.$store.state.isEmployee" to="/HomeEmp">Home</router-link>
        </b-nav-item>
        <b-nav-item>
          <router-link v-show="!this.$store.state.isEmployee" to="/Department">Department</router-link>
        </b-nav-item>
        <b-nav-item>
          <router-link v-show="this.$store.state.isEmployee" to="/CheckIn">CheckIn</router-link>
        </b-nav-item>
         <b-nav-item>
          <router-link v-show="!this.$store.state.isEmployee" to="/Pending">Pending</router-link>
        </b-nav-item>     
         <b-nav-item>
          <router-link  to="/PersonalInfo">Information</router-link>
        </b-nav-item>     
         <b-nav-item>
          <router-link v-show="!this.$store.state.isEmployee" to="/Assigne">Assigne</router-link>
        </b-nav-item>  
        <b-nav-item>
          <router-link v-show="this.$store.state.isEmployee" to="/Task">Task</router-link>
        </b-nav-item>    
      </b-navbar-nav>
      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">
        <b-nav-form>
          <span class="name-user">Hi, {{this.$store.state.userInfo.hoTen}}</span>
          <b-button id="logout" @click="logOut" v-show="this.$store.state.isLoggedIn" variant="outline-danger">
            <b-icon icon="power" aria-hidden="true"></b-icon>Logout
            </b-button>
          <b-form-input type="search" v-model="searchQuery" size="sm" class="mr-sm-2" placeholder="Search for name"></b-form-input>
          <b-button variant="secondary" size class="my-2 my-sm-0" @click="search" ><b-icon icon="search"></b-icon> Search</b-button>
        </b-nav-form>
      </b-navbar-nav>
    </b-navbar>
</template>
<script>
 import {mapActions} from 'vuex';
export default {
    data() {
        return {
            searchQuery:''
        }
    },
    methods: {   
        logOut(){
            this.$store.state.isLoggedIn=false;
            localStorage.removeItem("user_loggedin")
            this.$router.push('/login')          
        },    
        search(){
          EventBus.$emit('user-searched', this.searchQuery)

        }  
    },
    
}
</script>
<style scoped>
.bg-info {
    background-color: #2e3536 !important;
}
.name-user{
  color: aliceblue;
  padding-right:0.75rem;
  }
</style>