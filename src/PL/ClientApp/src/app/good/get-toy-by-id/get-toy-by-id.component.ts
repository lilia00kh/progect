import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { RepositoryService } from '../../shared/services/repository.service';
import { ToyModel } from '../../_interfaces/toy/toyModel';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { ImageModel } from 'src/app/_interfaces/imageModel';

@Component({
  selector: 'app-get-toy-by-id',
  templateUrl: './get-toy-by-id.component.html',
  styleUrls: ['./get-toy-by-id.component.css']
})
export class GetToyByIdComponent implements OnInit {

  public toy: ToyModel;
  public errorMessage = '';
  public showError: boolean;
  public id : string;
  images: ImageModel[];

  constructor(
    private route: ActivatedRoute,
    private repository: RepositoryService
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.getToy(this.id);
  }

  public getToy = (id:string) => {
    const apiAddress = 'api/toys/getbyid?id='+id;
    this.repository.getData(apiAddress)
      .subscribe(res => {
        this.toy = res as ToyModel;
        this.images = this.toy.imageModels as ImageModel[];
      });
  }
  public createImgPath = (serverPath: string) => {
    return this.repository.createImgPath(serverPath);
  }

}
