using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class DraconicDestruction : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/DraconicDestruction");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Draconic Destruction");
		Item.width = 90;  //The width of the .png file in pixels divided by 2.
		Item.damage = 270;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 24;
		Item.useStyle = 1;
		////Tooltip.SetDefault("Fires a draconic sword beam that explodes into additional beams\nAdditional beams fly up and down to shred enemies");
		Item.useTime = 24;  //Ranges from 1 to 55.
		Item.useTurn = true;
		Item.knockBack = 7.25f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 90;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.expert = true;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("DracoBeam").Type;
		Item.shootSpeed = 14f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ShadowspecBar", 5);
		recipe.AddIngredient(null, "CoreofCinder", 3);
		recipe.AddIngredient(null, "CoreofEleum", 3);
		recipe.AddIngredient(ItemID.FragmentSolar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 35);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	target.AddBuff(BuffID.OnFire, 500);
	}
}}
