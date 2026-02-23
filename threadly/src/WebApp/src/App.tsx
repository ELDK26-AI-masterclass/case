import { useMemo, useState } from 'react'
import './App.css'

type CategoryKey = 'All' | 'Tech' | 'Home' | 'Wellness' | 'Lifestyle'

type Product = {
  id: string
  name: string
  category: Exclude<CategoryKey, 'All'>
  price: number
  rating: number
  description: string
  accent: string
  tag: string
}

const categories: CategoryKey[] = ['All', 'Tech', 'Home', 'Wellness', 'Lifestyle']

const products: Product[] = [
  {
    id: 'p-01',
    name: 'Luma Desk Lamp',
    category: 'Home',
    price: 79,
    rating: 4.8,
    description: 'Dimmable alloy lamp with warm-night mode and USB-C charging base.',
    accent: '#9ca3af',
    tag: 'Best Seller',
  },
  {
    id: 'p-02',
    name: 'Pulse ANC Earbuds',
    category: 'Tech',
    price: 129,
    rating: 4.6,
    description: 'Adaptive noise cancellation and 30-hour battery for focused sessions.',
    accent: '#6b7280',
    tag: 'New',
  },
  {
    id: 'p-03',
    name: 'Aroma Diffuser Set',
    category: 'Wellness',
    price: 49,
    rating: 4.9,
    description: 'Ultrasonic diffuser with three botanical blends and silent operation.',
    accent: '#d1d5db',
    tag: 'Limited',
  },
  {
    id: 'p-04',
    name: 'Terra Ceramic Mug',
    category: 'Lifestyle',
    price: 24,
    rating: 4.7,
    description: 'Hand-finished mug with heat-retaining clay and matte glaze.',
    accent: '#9ca3af',
    tag: 'Editor Pick',
  },
  {
    id: 'p-05',
    name: 'Orbit Charging Stand',
    category: 'Tech',
    price: 89,
    rating: 4.5,
    description: 'Magnetic 3-in-1 stand for phone, watch, and earbuds with cable tray.',
    accent: '#4b5563',
    tag: 'Trending',
  },
  {
    id: 'p-06',
    name: 'Cloud Throw Blanket',
    category: 'Home',
    price: 64,
    rating: 4.8,
    description: 'Breathable woven blanket with recycled fibers and brushed finish.',
    accent: '#6b7280',
    tag: 'Top Rated',
  },
]

const highlights = [
  { label: 'Free Shipping', value: 'Orders over $50' },
  { label: 'Delivery Time', value: '2-4 business days' },
  { label: 'Customer Score', value: '4.8/5 average rating' },
]

const stories = [
  {
    title: 'Curated Product Drops',
    copy: 'Every week we publish a fresh collection of products selected by product designers and makers.',
  },
  {
    title: 'Quality First',
    copy: 'Each item is reviewed for build quality, durability, and practical day-to-day use.',
  },
  {
    title: 'No-Nonsense Returns',
    copy: '30-day returns with prepaid labels and same-day refund processing for eligible orders.',
  },
]

function App() {
  const [activeCategory, setActiveCategory] = useState<CategoryKey>('All')
  const [cart, setCart] = useState<Record<string, number>>({})

  const filteredProducts = useMemo(
    () => products.filter((product) => activeCategory === 'All' || product.category === activeCategory),
    [activeCategory],
  )

  const cartItems = useMemo(
    () =>
      products
        .filter((product) => cart[product.id])
        .map((product) => ({
          ...product,
          quantity: cart[product.id],
          total: cart[product.id] * product.price,
        })),
    [cart],
  )

  const cartCount = useMemo(() => Object.values(cart).reduce((sum, quantity) => sum + quantity, 0), [cart])

  const subtotal = useMemo(
    () => cartItems.reduce((sum, item) => sum + item.total, 0),
    [cartItems],
  )

  const shipping = subtotal === 0 || subtotal >= 50 ? 0 : 8
  const orderTotal = subtotal + shipping

  const addToCart = (productId: string) => {
    setCart((previous) => ({
      ...previous,
      [productId]: (previous[productId] ?? 0) + 1,
    }))
  }

  const removeFromCart = (productId: string) => {
    setCart((previous) => {
      const nextQuantity = (previous[productId] ?? 0) - 1

      if (nextQuantity <= 0) {
        const { [productId]: _, ...rest } = previous

        return rest
      }

      return {
        ...previous,
        [productId]: nextQuantity,
      }
    })
  }

  return (
    <div className="shop-page">
      <header className="shop-header">
        <a href="#" className="shop-brand" aria-label="Northstar market home">
          Northstar Market
        </a>
        <nav className="shop-nav" aria-label="Primary">
          <a href="#shop">Shop</a>
          <a href="#stories">Stories</a>
          <a href="#cart">Cart</a>
        </nav>
        <button type="button" className="shop-btn shop-cart-pill" aria-label="Open cart">
          Cart ({cartCount})
        </button>
      </header>

      <main className="shop-main">
        <section className="hero" id="shop">
          <div className="hero-copy">
            <span className="hero-kicker">Spring Collection 2026</span>
            <h1>Design-forward essentials for your desk, home, and everyday rhythm.</h1>
            <p>
              Shop curated products from independent makers, with fast delivery and quality backed by our in-house review team.
            </p>
            <div className="hero-actions">
              <button type="button" className="shop-btn">
                Shop New Arrivals
              </button>
              <button type="button" className="shop-btn-secondary">
                View Collections
              </button>
            </div>
          </div>
          <div className="hero-highlights">
            {highlights.map((item) => (
              <article key={item.label} className="highlight-card">
                <span>{item.label}</span>
                <strong>{item.value}</strong>
              </article>
            ))}
          </div>
        </section>

        <section className="shop-layout">
          <div>
            <div className="category-strip" role="tablist" aria-label="Product categories">
              {categories.map((category) => (
                <button
                  key={category}
                  type="button"
                  role="tab"
                  aria-selected={activeCategory === category}
                  className={`category-chip ${activeCategory === category ? 'is-active' : ''}`}
                  onClick={() => setActiveCategory(category)}
                >
                  {category}
                </button>
              ))}
            </div>

            <div className="product-grid">
              {filteredProducts.map((product) => (
                <article key={product.id} className="product-card">
                  <div className="product-media" style={{ background: product.accent }} aria-hidden="true" />
                  <div className="product-body">
                    <div className="product-head">
                      <span className="shop-tag">{product.tag}</span>
                      <span className="product-rating">{product.rating.toFixed(1)} ★</span>
                    </div>
                    <h2>{product.name}</h2>
                    <p>{product.description}</p>
                    <div className="product-foot">
                      <strong>${product.price}</strong>
                      <button type="button" className="shop-btn-secondary shop-btn-sm" onClick={() => addToCart(product.id)}>
                        Add to Cart
                      </button>
                    </div>
                  </div>
                </article>
              ))}
            </div>
          </div>

          <aside className="cart-panel" id="cart" aria-label="Cart summary">
            <h3>Your Cart</h3>
            <p>{cartCount === 0 ? 'No items added yet.' : `${cartCount} item${cartCount === 1 ? '' : 's'} selected`}</p>

            <div className="cart-lines">
              {cartItems.length > 0 ? (
                cartItems.map((item) => (
                  <div key={item.id} className="cart-line">
                    <div>
                      <strong>{item.name}</strong>
                      <span>
                        ${item.price} x {item.quantity}
                      </span>
                    </div>
                    <div className="cart-line-actions">
                      <span>${item.total}</span>
                      <button type="button" className="shop-btn-ghost shop-btn-sm" onClick={() => removeFromCart(item.id)}>
                        Remove
                      </button>
                    </div>
                  </div>
                ))
              ) : (
                <div className="cart-empty">Add products to preview your total and checkout details.</div>
              )}
            </div>

            <div className="cart-totals">
              <div>
                <span>Subtotal</span>
                <strong>${subtotal}</strong>
              </div>
              <div>
                <span>Shipping</span>
                <strong>{shipping === 0 ? 'Free' : `$${shipping}`}</strong>
              </div>
              <div className="cart-total-row">
                <span>Total</span>
                <strong>${orderTotal}</strong>
              </div>
            </div>

            <button type="button" className="shop-btn cart-checkout" disabled={cartCount === 0}>
              Checkout
            </button>
          </aside>
        </section>

        <section className="stories" id="stories">
          {stories.map((story) => (
            <article key={story.title} className="story-card">
              <h3>{story.title}</h3>
              <p>{story.copy}</p>
            </article>
          ))}
        </section>
      </main>
    </div>
  )
}

export default App
