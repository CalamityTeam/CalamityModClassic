using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons   //where is located
{
    public class ProfanedSword : ModItem
    {
    	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
		{
			//texture =("CalamityModClassic1Point1/Items/Weapons/ProfanedSword");
			return true;
		}
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Profaned Sword");     //Sword name
            Item.damage = 62;            //Sword damage
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            //if it's melee
            Item.width = 44;              //Sword width
            Item.height = 50;             //Sword height
            ////Tooltip.SetDefault("Summons brimstone geysers on enemy hits");  //Item Description
            Item.useTime = 23;          //how fast 
            Item.useAnimation = 23;  
			Item.useTurn = true;            
            Item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            Item.knockBack = 7.5f;      //Sword knockback
            Item.value = 300000;        
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;       //1 is the sound of the sword
            Item.autoReuse = true;   //if it's capable of autoswing.
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
        	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Brimblast").Type, hit.Damage, hit.Knockback, Main.myPlayer);
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