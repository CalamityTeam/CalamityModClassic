using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AbyssBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AbyssBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Abyss Blade");
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 88;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useTime = 20;  //Ranges from 1 to 55. 
		////Tooltip.SetDefault("Hitting enemies will cause the crush depth debuff\nThe lower the enemies' defense the more damage they take from this debuff");
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 54;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 900000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("DepthOrb").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DepthBlade");
        recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddIngredient(null, "CoreofEleum", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 33);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 300);
	}
}}
