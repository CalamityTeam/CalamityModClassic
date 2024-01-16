using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AnarchyBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/AnarchyBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Anarchy Blade");
		Item.width = 92;  //The width of the .png file in pixels divided by 2.
		Item.damage = 90;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 21;
		Item.useTime = 21;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		////Tooltip.SetDefault("The lower your life the more damage this blade does\nYour hits will generate a large explosion\nIf you're below 50% life your hits have a chance to instantly kill regular enemies");
		Item.useStyle = 1;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 92;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.rare = 8;  //Ranges from 1 to 11.
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UnholyCore", 5);
        recipe.AddIngredient(null, "CoreofChaos", 3);
        recipe.AddIngredient(ItemID.BrokenHeroSword);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 235);
        }
    }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
    	int lifeAmount = player.statLifeMax2 - player.statLife;
    	float damageAdd = (((float)lifeAmount * 0.1f) + 90f);
        damage.Base = (int)damageAdd;
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	Projectile.NewProjectile(Item.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BrimstoneBoom").Type, (int)((double)hit.Damage * 0.5f), hit.Knockback, Main.myPlayer);
    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
    	if (player.statLife < (player.statLifeMax2 * 0.5f) && Main.rand.Next(5) == 0 && !target.boss)
    	{
    		target.life = 0;
            target.HitEffect(0, 10.0);
            target.active = false;
            target.NPCLoot();
    	}
	}
}}
