using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
    public class DragonPow : ModItem
    {
        public static int BaseDamage = 6600;
        public static float Speed = 13f;
        public static float ReturnSpeed = 20f;
        public static float SparkSpeed = 0.6f;
        public static float MinPetalSpeed = 24f;
        public static float MaxPetalSpeed = 30f;
        public static float MinWaterfallSpeed = 12f;
        public static float MaxWaterfallSpeed = 15.5f;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Pow");
        }

        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 82;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.damage = BaseDamage;
            Item.knockBack = 9f;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.useStyle = 5;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Custom/YharonRoarShort", SoundType.Sound);
            Item.channel = true;

            Item.rare = 10;
            Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
            Item.value = Item.buyPrice(1, 80, 0, 0);

            Item.shoot = Mod.Find<ModProjectile>("DragonPowFlail").Type;
            Item.shootSpeed = Speed;
        }

        public override void AddRecipes()
        {
            Recipe r = CreateRecipe();
            r.AddTile(null, "DraedonsForge");
            r.AddIngredient(null, "Mourningstar");
            r.AddIngredient(ItemID.DaoofPow);
            r.AddIngredient(ItemID.FlowerPow);
            r.AddIngredient(ItemID.Flairon);
            r.AddIngredient(null, "BallOFugu");
            r.AddIngredient(null, "Tumbleweed");
            r.AddIngredient(null, "UrchinFlail");
            r.AddIngredient(null, "CosmiliteBar", 5);
            r.AddIngredient(null, "Phantoplasm", 5);
            r.AddIngredient(null, "NightmareFuel", 5);
            r.AddIngredient(null, "EndothermicEnergy", 5);
            r.AddIngredient(null, "DarksunFragment", 5);
            r.AddIngredient(null, "HellcasterFragment", 10);
            r.AddIngredient(null, "AuricOre", 25);
            r.Register();
        }
    }
}
