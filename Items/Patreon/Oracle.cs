using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
    public class Oracle : ModItem
    {
        public static int BaseDamage = 1360;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Oracle");
            // Tooltip.SetDefault("Emits an aura of red lightning\nFires auric orbs when supercharged\nHitting enemies charges the yoyo\n'Gaze into the past, the present, the future... and the circumstances of your inevitable demise'");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Kraken);

            Item.width = 54;
            Item.height = 42;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.damage = BaseDamage;
            Item.knockBack = 4f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.useStyle = 5;
            Item.channel = true;
            
            Item.rare = 10;
            Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
            Item.value = Item.buyPrice(1, 80, 0, 0);

            Item.shoot = Mod.Find<ModProjectile>("OracleYoyo").Type;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddTile(null, "DraedonsForge");
            r.AddIngredient(null, "TheObliterator");
            r.AddIngredient(null, "Lacerator");
            r.AddIngredient(null, "Verdant");
            r.AddIngredient(null, "Chaotrix");
            r.AddIngredient(null, "Quagmire");
            r.AddIngredient(null, "Shimmerspark");
            r.AddIngredient(null, "NightmareFuel", 5);
            r.AddIngredient(null, "EndothermicEnergy", 5);
            r.AddIngredient(null, "CosmiliteBar", 5);
            r.AddIngredient(null, "DarksunFragment", 5);
            r.AddIngredient(null, "Phantoplasm", 5);
            r.AddIngredient(null, "HellcasterFragment", 3);
            r.AddIngredient(null, "AuricOre", 25);
            r.Register();
        }
    }
}
