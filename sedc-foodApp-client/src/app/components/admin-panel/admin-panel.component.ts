import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Municipality, RestaurantRequestModel } from 'src/app/models/restaurant-model';
import { AdminPanelService } from 'src/app/services/admin-panel.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  modalRef: BsModalRef;

  restaurants: any;

  restaurantId: string;

  isEditMode: boolean = false;

  requestForm = new FormGroup({
    name: new FormControl('', Validators.required),
    address: new FormControl(''),
    municipality: new FormControl('')
  })

  filterForm = new FormGroup({
    name: new FormControl(''),
    address: new FormControl(''),
  })

  municipalityFlterForm = new FormGroup({
    municipality: new FormControl('')
  })

  municipalityList = [Municipality.karpos, Municipality.centar, Municipality.aerodrom]

  constructor(private adminPanelService: AdminPanelService,
              private modalService: BsModalService) {}

  ngOnInit(): void {
    this.getAllRestaurants()
  }

  addRestaurant() {
    let requestModel = new RestaurantRequestModel();
    requestModel.name = this.requestForm.value.name
    requestModel.address = this.requestForm.value.address
    requestModel.municipality = parseInt(this.requestForm.value.municipality)

    this.adminPanelService.addRestaurant(requestModel).subscribe({
      error: err => console.warn(err.error),
      complete: () => {
        this.closeModal()
        this.getAllRestaurants()
      }
    })
  }

  getAllRestaurants() {
    let filter = {
      name: this.filterForm.value.name,
      address: this.filterForm.value.address,
      municipality: this.municipalityFlterForm.value.municipality
    }

    this.adminPanelService.getAllRestaurants(filter).subscribe({
      next: res => {
        this.restaurants = res
      }
    })
  }

  updateRestaurant() {
    let body = Object.assign(this.requestForm.value, { id : this.restaurantId})
    body.municipality = parseInt(body.municipality)

    this.adminPanelService.updateRestaurant(body).subscribe({
      error: err => console.warn(err.error),
      complete: () => {
        this.closeModal()
        this.getAllRestaurants()
      }
    })
  }

  deleteRestaurant(id: string) {
    this.adminPanelService.deleteRestaurant(id).subscribe({
      error: err => console.warn(err.error),
      complete: () => {
        this.getAllRestaurants()
      }
    })
  }

  openModal(template: TemplateRef<any>, restaurant?: any) {
    this.modalRef = this.modalService.show(template);

    if(!restaurant) {
      this.requestForm.get("municipality").setValue(Municipality.karpos)
    }

    if(!!restaurant) {
      this.isEditMode = true;
      const {id, menu, ...rest} = restaurant

      this.requestForm.setValue(rest)
      this.restaurantId = id
    }
  }

  closeModal() {
    this.modalService._hideModal()
    this.modalService._hideBackdrop()
    this.isEditMode = false;
    this.requestForm.reset()
  }
}
