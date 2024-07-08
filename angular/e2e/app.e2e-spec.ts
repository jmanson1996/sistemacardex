import { FactoraTemplatePage } from './app.po';

describe('Factora App', function() {
  let page: FactoraTemplatePage;

  beforeEach(() => {
    page = new FactoraTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
