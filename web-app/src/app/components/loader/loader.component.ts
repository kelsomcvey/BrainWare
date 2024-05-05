import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";


@Component({
    standalone: true,
    imports: [CommonModule],
    selector: 'bw-app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.scss']
})

export class LoaderComponent  {}