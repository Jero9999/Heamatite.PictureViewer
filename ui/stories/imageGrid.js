import { html, LitElement } from 'lit';
import { ref, createRef } from 'lit/directives/ref.js';
import '../components/imageGrid';
import capral from './assets/images/capral.png'

class ImageGridLoader extends LitElement {

	static properties = {
		images: { type: Array },
	};

	constructor() {
		super();
		this.images = Array(200).fill(capral);
	}

	render() {
		return html`<image-grid .images=${this.images}></image-grid>`;

	}
}

customElements.define('image-grid-loader', ImageGridLoader);

export const ImageGrid = (imageList) => html`<image-grid-loader></image-grid-loader>`;