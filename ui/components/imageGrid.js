import { html, css, LitElement } from 'lit';

class ImageGrid extends LitElement {
  static styles = css`
    .container {
      display: flex;
      flex-wrap: wrap;
      gap: 10px;
    }

    .image {
      width: 100%;
      height: auto;
    }
  `;

  static properties = {
    images: { type },
  };

  constructor() {
    super();
    this.images = [];
  }

  render() {
    return html`
      <div class="container">
        ${this.images.map(
          (image) => html`
            <img class="image" src="${image}" alt="Image" />
          `
        )}
      </div>
    `;
  }
}

customElements.define('image-grid', ImageGrid);
