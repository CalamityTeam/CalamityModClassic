using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class ProfanedSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimstone Sword");
			// Tooltip.SetDefault("Summons brimstone geysers on enemy hits");
		}

        public override void SetDefaults()
        {
            Item.damage = 61;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 50;
            Item.useTime = 23;
            Item.useAnimation = 23;
			Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Brimblast").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
            }
        }
        
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
    }
}